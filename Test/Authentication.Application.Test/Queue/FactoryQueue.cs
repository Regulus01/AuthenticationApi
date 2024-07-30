using Domain.Authentication.QueueEvents;
using MassTransit;
using Moq;

namespace AuthenticationTests.Queue;

public static class FactoryQueue
{
    public static InserirUltimoLoginEvent InserirUltimoLoginEvent(Guid? usuarioId = null, DateTimeOffset? data = null)
    {
        return new InserirUltimoLoginEvent
        {
            UsuarioId = usuarioId ?? Guid.Empty,
            DataDoUltimoLogin = data ?? DateTimeOffset.Now
        };
    }
        
    public static ConsumeContext<InserirUltimoLoginEvent> Consume(InserirUltimoLoginEvent dto) 
    {
        var consumer = new Mock<ConsumeContext<InserirUltimoLoginEvent>>();

        consumer.Setup(x => x.Message).Returns(dto);
        consumer.Setup(x => x.CancellationToken).Returns(new CancellationToken());

        return consumer.Object;
    }
}