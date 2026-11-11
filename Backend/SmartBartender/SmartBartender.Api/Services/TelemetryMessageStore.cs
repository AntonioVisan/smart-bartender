using System.Collections.Concurrent;

namespace SmartBartender.Api.Services;

public class TelemetryMessageStore
{
    private readonly ConcurrentQueue<TelemetryMessage> _messages = new();

    public void Add(TelemetryMessage message)
    {
        _messages.Enqueue(message);

        while (_messages.Count > 100)
        {
            _messages.TryDequeue(out _);
        }
    }

    public IEnumerable<TelemetryMessage> GetAll()
    {
        return _messages.ToArray().Reverse();
    }
}
