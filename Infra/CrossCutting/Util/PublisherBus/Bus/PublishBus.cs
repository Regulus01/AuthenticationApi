using MassTransit;

namespace PublisherBus.Bus;

public class PublishBus : IPublishBus
{
    private readonly IBus _bus;

    public PublishBus(IBus bus)
    {
        _bus = bus;
    }

    public Task PublishAsync<T>(T message, CancellationToken ct = default) where T : class
    {
        return _bus.Publish(message, ct);
    }
}