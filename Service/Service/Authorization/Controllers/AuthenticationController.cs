using System.ComponentModel.DataAnnotations;
using Application.Authorization.Dto;
using Application.Authorization.Interface;
using Infra.CrossCutting.Util.Configuration.Core.Controllers;
using Infra.CrossCutting.Util.Notifications.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Service.Authorization.Controllers;

[ApiController]
[Route("api/Authentication/")]
public class AuthenticationController : CoreController
{
    private readonly IAuthorizationAppService _appService;

    public AuthenticationController(INotify notification, IAuthorizationAppService appService) : base(notification)
    {
        _appService = appService;
    }

    /// <summary>
    /// Autentica o usuário no sistema
    /// </summary>
    /// <param name="login">Dados necessários para o login no sistema</param>
    /// <returns>
    ///  Token de acesso do usuário
    /// </returns>
    /// <response code="200">Usuário autenticado</response>
    /// <response code="400">Erro na requisição</response>
    [ProducesResponseType(typeof(ReponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ReponseModel), StatusCodes.Status400BadRequest)]
    [HttpPost]
    [Route("Login")]
    [AllowAnonymous]
    public IActionResult ObterTokenDeAutenticacao(LoginDto login)
    {
        var response = _appService.Login(login);

        return Response(response);
    }

    /// <summary>
    /// Insere uma data para ser o ultimo login do usuário
    /// </summary>
    /// <param name="usuarioId">Id do usuário</param>
    /// <param name="dto"></param>
    /// <response code="200">Editado com sucesso</response>
    /// <response code="400">Erro na requisição</response>
    [ProducesResponseType(typeof(ReponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ReponseModel), StatusCodes.Status400BadRequest)]
    [HttpPatch]
    [Route("{usuarioId:guid}/UltimoLogin")]
    [AllowAnonymous]
    public IActionResult InserirDataDoLogin([Required][FromRoute] Guid usuarioId, InserirUltimoLoginDto dto)
    {
        _appService.InserirUltimoLogin(usuarioId, dto);
        return Response();
    }

    /// <summary>
    /// Cadastra um usuário no sistema
    /// </summary>
    /// <param name="dto">Dados necessários para o cadastro no sistema</param>
    /// <returns>
    ///  Indicativo se a operação foi bem sucedida
    /// </returns>
    /// <response code="200">Usuário cadastrado</response>
    /// <response code="400">Erro na requisição</response>
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