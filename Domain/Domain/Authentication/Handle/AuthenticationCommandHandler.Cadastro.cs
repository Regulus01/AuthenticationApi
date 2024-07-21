using Domain.Authentication.Commands;
using Domain.Authentication.Entities;
using Domain.Authentication.Entities.Roles;
using Infra.CrossCutting.Util.Notifications.Resourcers;
using Microsoft.AspNetCore.Identity;

namespace Domain.Authentication.Handle;

public partial class AuthenticationCommandHandler
{
    public Task Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = _mapper.Map<Usuario>(request);
        
        CadastroComplementar(ref usuario);

        AtribuirRolePadraoAoUsuario(ref usuario);
        
        _usuarioRepository.AdicionarUsuario(usuario);
        
        if (!_usuarioRepository.Commit())
        {
            _notify.NewNotification("Erro", ResourceErrorMessage.FALHA_NO_COMMIT);
            return Task.FromResult(cancellationToken);
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Preenche os campos de data de cadastro, status e cria um hash da senha
    /// </summary>
    /// <param name="usuario">Usuário com dados para a criação do hash</param>
    private void CadastroComplementar(ref Usuario usuario)
    {
        usuario.InformeSenha(HashSenha(usuario));
        usuario.InformeDataDeCadastro(DateTimeOffset.UtcNow);
        usuario.InformeStatus(true);
    }

    /// <summary>
    /// Cria uma hash da senha do usuário
    /// </summary>
    /// <param name="usuario">usuario com dados para criar o hash</param>
    /// <returns>Hash da senha</returns>
    private string HashSenha(Usuario usuario)
    {
        var passwordHasher = new PasswordHasher<Usuario>();
        return passwordHasher.HashPassword(usuario, usuario.Password);
    }
    
    /// <summary>
    /// Atribui crole padrão para um usuário
    /// </summary>
    /// <param name="usuario">usuario a ter a role atribuida</param>
    private void AtribuirRolePadraoAoUsuario(ref Usuario usuario)
    {
        var usuarioRole = new UsuarioRole(usuario.Id, Guid.Parse(RoleRegister.Usuario.Id));
        usuario.UsuarioRoles.Add(usuarioRole);
    }
}