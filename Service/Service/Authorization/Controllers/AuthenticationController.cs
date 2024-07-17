using Application.Authorization.Dto;
using Application.Authorization.Interface;
using Application.ViewModels;
using HttpAcessor;
using Infra.CrossCutting.Util.Configuration.Core.Controllers;
using Infra.CrossCutting.Util.Notifications.Interface;
using Infra.CrossCutting.Util.Notifications.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Service.Authorization.Controllers;

[ApiController]
[Route("api/Authentication/")]
public class AuthenticationController : CoreController
{
    private IAuthorizationAppService _appService;

    public AuthenticationController(INotify notification, IAuthorizationAppService appService) : base(notification)
    {
        _appService = appService;
    }

    /// <summary>
    /// EndPoint utilizado para logar no sistema
    /// </summary>
    /// <remarks>
    /// Utilizado para realizar o login no sistema
    /// </remarks>
    /// <param name="login">Dados necessários para o login no sistema</param>
    /// <returns>
    ///  Token de acesso do usuário
    /// </returns>
    /// <response code="200">Retorna quando usuário é autenticado com sucesso</response>
    [HttpPost]
    [Route("Login")]
    [AllowAnonymous]
    public IActionResult ObterTokenDeAutenticacao(LoginViewModel? login)
    {
        var response = _appService.Login(login);

        return Response(response);
    }

    /// <summary>
    /// EndPoint utilizado para cadastrar usuário no sistema
    /// </summary>
    /// <remarks>
    /// EndPoint utilizado para cadastrar usuário no sistema, o usuário cadastrado por padrão terá a role de usuario
    /// </remarks>
    /// <param name="dto">Dados necessários para o cadastro no sistema</param>
    /// <returns>
    ///  Indicativo se a operação foi bem sucedida
    /// </returns>
    [ProducesResponseType(typeof(ReponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ReponseModel), StatusCodes.Status400BadRequest)]
    [HttpPost]
    [Route("Cadastrar")]
    [AllowAnonymous]
    public IActionResult CadastrarUsuario(CadastrarUsuarioDto dto)
    {
        _appService.CadastrarUsuario(dto);

        return Response();
    }
}