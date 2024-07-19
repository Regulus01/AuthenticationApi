using System.Linq.Expressions;
using Application.Authorization.AppService;
using Application.AutoMapper;
using Domain.Authentication.Entities;
using Domain.Authentication.Interface;
using Infra.CrossCutting.Util.Notifications.Interface;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
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

    public void SetupObterUsuario(IEnumerable<Usuario> usuarios)
    {
        Mocker.GetMock<IUsuarioRepository>()
            .Setup(x => x.ObterUsuario(It.IsAny<Expression<Func<Usuario, bool>>>(),
                It.IsAny<Func<IQueryable<Usuario>, IIncludableQueryable<Usuario, object>>?>()))
            .Returns<Expression<Func<Usuario, bool>>, Func<IQueryable<Usuario>, IIncludableQueryable<Usuario, object>>?>
                ((predicate, _) => usuarios.FirstOrDefault(predicate.Compile()));
    }

    public void SetupHasNotifications(bool hasNotifications = true)
    {
        Mocker.GetMock<INotify>()
            .Setup(x => x.HasNotifications())
            .Returns(hasNotifications);
    }
}