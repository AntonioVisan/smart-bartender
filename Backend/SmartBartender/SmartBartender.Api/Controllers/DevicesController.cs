using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices;
using SmartBartender.Api.Services;
using System.Text;

namespace SmartBartender.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevicesController : ControllerBase
{
    private readonly TelemetryMessageStore _telemetryMessageStore;
    private readonly ServiceClient _serviceClient;

    public DevicesController(
        TelemetryMessageStore telemetryMessageStore, 
        ServiceClient serviceClient)
    {
        _telemetryMessageStore = telemetryMessageStore;
        _serviceClient = serviceClient;
    }

    [HttpGet("telemetry")]
    public IActionResult GetTelemetry()
    {
        var messages = _telemetryMessageStore.GetAll();

        return Ok(messages);
    }

    [HttpPost("{deviceId}/message")]
    public async Task<IActionResult> ForwardMessageAsync(
        string deviceId,
        [FromBody] string messageText)
    {
        using Message message = new(Encoding.UTF8.GetBytes(messageText))
        {
            ContentType = "text/plain",
            ContentEncoding = "utf-8"
        };

        await _serviceClient.SendAsync(deviceId, message);

        return Ok();
    }
}
