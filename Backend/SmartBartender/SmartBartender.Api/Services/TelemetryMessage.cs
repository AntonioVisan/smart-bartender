namespace SmartBartender.Api.Services;

public class TelemetryMessage
{
    public string DeviceId { get; set; } = "";
    public string Body { get; set; } = "";
    public DateTime ReceivedAt { get; set; }
}
