using Application.Authorization.Dto;
using Application.ViewModels;

namespace Application.Authorization.Interface;

public interface IAuthorizationAppService
{
    /// <summary>
    /// Obtem o token de autenticação do usuário
    /// </summary>
    /// <param name="dto">Dados necessários para o login</param>
    /// <returns>Token de autenticação do usuário</returns>
    TokenViewModel Login(LoginDto dto);

    /// <summary>
    /// Insere uma data para ser utilizada como ultimo login do usuário
    /// </summary>
    /// <param name="usuarioId">Id do usuário</param>
    /// <param name="dto">Dados a serem registrados ao realizar o login</param>
    void InserirUltimoLogin(Guid usuarioId, InserirUltimoLoginDto dto);
    
    /// <summary>
    /// Cadastra um usuário no sistema
    /// </summary>
    /// <remarks>
    /// Este método cria um novo registro de usuário no sistema. 
    /// O usuário cadastrado receberá por padrão a role de "usuário".
    /// </remarks>
    /// <param name="dto">Dados necessários para o cadastro do usuário</param>
    void CadastrarUsuario(CadastrarUsuarioDto dto);
}