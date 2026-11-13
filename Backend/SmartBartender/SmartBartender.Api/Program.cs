using Microsoft.Azure.Devices;
using SmartBartender.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<TelemetryMessageStore>();
builder.Services.AddSingleton<AzureAiAssistantService>();
builder.Services.AddHostedService<IoTHubTelemetryReader>();
builder.Services.AddSingleton<ServiceClient>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();

    var connectionString = configuration["IoTHub:ServiceConnectionString"]
        ?? throw new InvalidOperationException(
            "Missing IoT Hub Service connection string.");

    return ServiceClient.CreateFromConnectionString(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
