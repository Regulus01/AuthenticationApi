using System.Linq.Expressions;
using Application.Authorization.Interface;
using Domain.Authentication.Commands;
using Domain.Authentication.Entities;
using Domain.Authentication.Interface;
using Infra.CrossCutting.Util.Notifications.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Xunit;

namespace AuthenticationTests;

public class Test : IClassFixture<Fixture>
{
    private Fixture _fixture;
    private IAuthorizationAppService _appService;

    public Test(Fixture fixture)
    {
        _fixture = fixture;
        _appService = fixture.ObterAuthorizationAppService();
    }

    [Fact(DisplayName = "Cadastra o usuário - Sucesso")]
    public void CadastrarUsuario_Sucesso()
    {
        //Arrange
        var cadastroDto = Factory.CadastrarUsuarioDto();

        //Act
        _appService.CadastrarUsuario(cadastroDto);

        //Assert
        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.HasNotifications(),
                Times.Once);

        _fixture.Mocker.GetMock<IUsuarioRepository>()
            .Verify(x => x.ObterUsuario(
                    It.IsAny<Expression<Func<Usuario, bool>>>(),
                    It.IsAny<Func<IQueryable<Usuario>, IIncludableQueryable<Usuario, object>>?>()),
                Times.Once);

        _fixture.Mocker.GetMock<IMediator>()
            .Verify(x => x.Send(
                    It.IsAny<CadastrarUsuarioCommand>(), It.IsAny<CancellationToken>()),
                Times.Once);
    }
}