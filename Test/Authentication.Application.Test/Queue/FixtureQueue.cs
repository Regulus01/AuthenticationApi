using Application.AutoMapper;
using Application.QueueConsumes;
using Infra.CrossCutting.Util.Notifications.Interface;
using Moq.AutoMock;

namespace AuthenticationTests.Queue;

public class FixtureQueue
{
    public AutoMocker Mocker { get; private set; }

    public InserirUltimoLoginEventConsumer ObterQueueConsumes()
    {
        Mocker = new AutoMocker();

        Mocker.Use(AutoMapperConfig.RegisterMaps().CreateMapper());

        var appService = Mocker.CreateInstance<InserirUltimoLoginEventConsumer>();

        return appService;
    }

    public void SetupHasNotifications(bool hasNotifications = true)
    {
        Mocker.GetMock<INotify>()
              .Setup(x => x.HasNotifications())
              .Returns(hasNotifications);
    }
}