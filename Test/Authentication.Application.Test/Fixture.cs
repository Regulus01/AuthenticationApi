using Application.Authorization.AppService;
using Application.AutoMapper;
using Moq.AutoMock;

namespace AuthenticationTests;

public class Fixture
{
    public AutoMocker Mocker { get; private set; }

    public AuthorizationAppService ObterAuthorizationAppService()
    {
        Mocker = new AutoMocker();

        Mocker.Use(AutoMapperConfig.RegisterMaps().CreateMapper());

        var appService = Mocker.CreateInstance<AuthorizationAppService>();

        return appService;
    }
}