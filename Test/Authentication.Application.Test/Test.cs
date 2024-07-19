using System.Linq.Expressions;
using Application.Authorization.Interface;
using Domain.Authentication.Commands;
using Domain.Authentication.Entities;
using Domain.Authentication.Interface;
using Infra.CrossCutting.Util.Notifications.Interface;
using Infra.CrossCutting.Util.Notifications.Resourcers;
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
    
    [Fact(DisplayName = "Cadastra o usuário - Email cadastrado - Falha")]
    public void CadastrarUsuario_EmailCadastrado_Falha()
    {
        //Arrange
        var cadastroDto = Factory.CadastrarUsuarioDto(email : "aline@gmail.com");
        
        var usuarioDoFiltro = Factory.UsuarioDomain(email: "aline@gmail.com");

        var usuarios = new []
        {
            Factory.UsuarioDomain(),
            Factory.UsuarioDomain(),
            usuarioDoFiltro,
            Factory.UsuarioDomain(),
        };
        
        _fixture.SetupObterUsuario(usuarios);

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
        
        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(
                    It.Is<string>(e => e.Equals("Erro")), 
                    It.Is<string>(e => e.Equals(ResourceErrorMessage.EMAIL_CADASTRADO))),
                Times.Once);

        _fixture.Mocker.GetMock<IMediator>()
            .Verify(x => x.Send(
                    It.IsAny<CadastrarUsuarioCommand>(), It.IsAny<CancellationToken>()),
                Times.Never);
    }
    
    [Fact(DisplayName = "Cadastra o usuário - Dados invalidos - Falha")]
    public void CadastrarUsuario_DadosInvalidos_Falha()
    {
        //Arrange
        var cadastroDto = Factory.CadastrarUsuarioDto(email : "aline@gmail", password: "");
        
        //Setup
        _fixture.SetupHasNotifications();
        
        //Act
        _appService.CadastrarUsuario(cadastroDto);

        //Assert
        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.HasNotifications(),
                Times.Once());
        
        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(
                    It.IsAny<string>(), 
                    It.IsAny<string>()),
                Times.Exactly(2));

        
        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(
                    It.Is<string>(e => e.Equals("Erro")), 
                    It.Is<string>(e => e.Equals(ResourceErrorMessage.FORMATO_EMAIL_INVALIDO))),
                Times.Once);
        
        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(
                    It.Is<string>(e => e.Equals("Erro")), 
                    It.Is<string>(e => e.Equals(ResourceErrorMessage.SENHA_VAZIA))),
                Times.Once);
        
        _fixture.Mocker.GetMock<IUsuarioRepository>()
            .Verify(x => x.ObterUsuario(
                    It.IsAny<Expression<Func<Usuario, bool>>>(),
                    It.IsAny<Func<IQueryable<Usuario>, IIncludableQueryable<Usuario, object>>?>()),
                Times.Never);

        _fixture.Mocker.GetMock<IMediator>()
            .Verify(x => x.Send(
                    It.IsAny<CadastrarUsuarioCommand>(), It.IsAny<CancellationToken>()),
                Times.Never);
    }
}