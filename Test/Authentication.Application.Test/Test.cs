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
    private readonly Fixture _fixture;
    private readonly IAuthorizationAppService _appService;

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
                    It.Is<CadastrarUsuarioCommand>(cmd => cmd.Email.Equals(cadastroDto.Email) && 
                                                          cmd.Password.Equals(cadastroDto.Password)), 
                    It.IsAny<CancellationToken>()),
                Times.Once);
    }

    [Fact(DisplayName = "Cadastra o usuário - Email cadastrado - Falha")]
    public void CadastrarUsuario_EmailCadastrado_Falha()
    {
        //Arrange
        var cadastroDto = Factory.CadastrarUsuarioDto(email: "aline@gmail.com");

        var usuarioDoFiltro = Factory.UsuarioDomain(email: "aline@gmail.com");

        var usuarios = new[]
        {
            Factory.UsuarioDomain(),
            Factory.UsuarioDomain(),
            usuarioDoFiltro,
            Factory.UsuarioDomain(),
        };

        //Setup
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

    [Theory(DisplayName = "Cadastra o usuário - Dados invalidos - Falha")]
    [InlineData("aline")]
    [InlineData("aline@")]
    [InlineData("@gmail.com")]
    public void CadastrarUsuario_DadosInvalidos_Falha(string email)
    {
        //Arrange
        var cadastroDto = Factory.CadastrarUsuarioDto(email: email, password: "");

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

    [Fact(DisplayName = "Login - Realiza Autenticacao - Sucesso")]
    public void Login_RealizaAutenticacao_Sucesso()
    {
        //Arrange
        var loginDto = Factory.LoginDto("aline@gmail.com", "123qw");
        
        //Act
        _appService.Login(loginDto);

        //Assert
        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.HasNotifications(),
                Times.Once);

        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Never);

        _fixture.Mocker.GetMock<IMediator>()
            .Verify(x => x.Send(
                    It.Is<LoginCommand>(cmd => cmd.Email.Equals(loginDto.Email) && 
                                               cmd.Password.Equals(loginDto.Password)),
                    It.IsAny<CancellationToken>()),
                Times.Once);
    }
    
    [Theory(DisplayName = "Login - Dados invalidos - Falha")]
    [InlineData("aline")]
    [InlineData("aline@")]
    [InlineData("@gmail.com")]
    public void Login_DadosInvalidos_Falha(string email)
    {
        //Arrange
        var loginDto = Factory.LoginDto(email: email, "");
        
        //Setup
        _fixture.SetupHasNotifications();
        
        //Act
        _appService.Login(loginDto);

        //Assert
        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.HasNotifications(),
                Times.Once);

        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Exactly(2));

        _fixture.Mocker.GetMock<IMediator>()
            .Verify(x => x.Send(
                    It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>()),
                Times.Never);
    }
}