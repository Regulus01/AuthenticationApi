using Application.QueueConsumes;
using Domain.Authentication.Commands;
using Infra.CrossCutting.Util.Notifications.Interface;
using Infra.CrossCutting.Util.Notifications.Resourcers;
using MediatR;
using Moq;
using Xunit;

namespace AuthenticationTests.Queue;

public class TestQueue : IClassFixture<FixtureQueue>
{
    private readonly FixtureQueue _fixture;
    private readonly InserirUltimoLoginEventConsumer _appService;

    public TestQueue(FixtureQueue fixture)
    {
        _fixture = fixture;
        _appService = fixture.ObterQueueConsumes();
    }
    
    [Fact(DisplayName = "InserirUltimoLogin - Sucesso")]
    public async Task InserirUltimoLogin_Sucesso()
    {
        //Arrange
        var dto = FactoryQueue.InserirUltimoLoginEvent(Guid.NewGuid());
        var consume = FactoryQueue.Consume(dto);

        //Act
        await _appService.Consume(consume);

        //Assert
        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.HasNotifications(),
                Times.Once());

        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Never());
        
        _fixture.Mocker.GetMock<IMediator>()
            .Verify(x => x.Send(
                    It.Is<InserirUltimoLoginCommand>(cmd => cmd.DataDoUltimoLogin == dto.DataDoUltimoLogin),
                    It.IsAny<CancellationToken>()),
                Times.Once);
    }
    
    [Fact(DisplayName = "InserirUltimoLogin - Sem id do usuário - Falha")]
    public async Task InserirUltimoLogin_SemIdDoUsuario_Falha()
    {
        //Arrange
        var dto = FactoryQueue.InserirUltimoLoginEvent();
        var consume = FactoryQueue.Consume(dto);

        //Setup
        _fixture.SetupHasNotifications();
        
        //Act
        await _appService.Consume(consume);

        //Assert
        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.HasNotifications(),
                Times.Once());

        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(
                    It.Is<string>(e => e.Equals("Erro")),
                    It.Is<string>(e => e.Equals(ResourceErrorMessage.USUARIO_VAZIO))),
                Times.Once());
        
        _fixture.Mocker.GetMock<IMediator>()
            .Verify(x => x.Send(
                    It.IsAny<InserirUltimoLoginCommand>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
    }
}