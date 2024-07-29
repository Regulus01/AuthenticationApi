using Domain.Authentication.Commands;
using Domain.Authentication.Configuration;
using Domain.Authentication.Entities;
using Domain.Authentication.QueueEvents;
using Infra.CrossCutting.Util.Notifications.Resourcers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Authentication.Handle;

public partial class AuthenticationCommandHandler
{
    public async Task<TokenModel> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var usuario = _usuarioRepository.ObterUsuario(x => x.Email.Equals(request.Email),
            query => query.Include(x => x.UsuarioRoles)
                .ThenInclude(y => y.Role));

        if (usuario == null)
        {
            _notify.NewNotification("Erro", ResourceErrorMessage.USUARIO_NAO_ENCONTRADO);
            return new TokenModel();
        }

        var passwordHasher = new PasswordHasher<Usuario>();

        if (passwordHasher.VerifyHashedPassword(usuario, usuario.Password, request.Password) ==
            PasswordVerificationResult.Failed)
        {
            _notify.NewNotification("Erro", ResourceErrorMessage.SENHA_INVALIDA);
            return new TokenModel();
        }

        var token = TokenService.GenerateToken(usuario);

        if (token == null)
        {
            _notify.NewNotification("Erro", ResourceErrorMessage.FALHA_AO_GERAR_TOKEN);
            return new TokenModel();
        }

        var evento = new InserirUltimoLoginEvent
        {
            UsuarioId = usuario.Id,
            DataDoUltimoLogin = DateTimeOffset.UtcNow
        };

        await _bus.PublishAsync(evento, cancellationToken);

        return token;
    }

    public Task Handle(InserirUltimoLoginCommand request, CancellationToken cancellationToken)
    {
        var usuario = _usuarioRepository.ObterUsuario(x => x.Id.Equals(request.UsuarioId));

        if (usuario == null)
        {
            _notify.NewNotification("Erro", ResourceErrorMessage.USUARIO_NAO_ENCONTRADO);
            return Task.FromResult(new TokenModel());
        }

        usuario.InformeUltimoLogin(request.DataDoUltimoLogin);

        _usuarioRepository.Update(usuario);

        if (!_usuarioRepository.Commit())
        {
            _notify.NewNotification("Erro", ResourceErrorMessage.FALHA_NO_COMMIT);
            return Task.FromResult(cancellationToken);
        }

        return Task.CompletedTask;
    }
}