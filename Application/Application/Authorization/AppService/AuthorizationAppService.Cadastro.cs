using Application.Authorization.Dto;
using Application.Authorization.Dto.Validators;
using Domain.Authentication.Commands;
using Infra.CrossCutting.Util.Notifications.Resourcers;

namespace Application.Authorization.AppService;

public partial class AuthorizationAppService
{
    /// <inheritdoc />
    public void CadastrarUsuario(CadastrarUsuarioDto dto)
    {
        ValidarCadastro(dto);

        if (_notify.HasNotifications())
            return;

        var usuario = _repository.ObterUsuario(x => x.Email.Equals(dto.Email));
        
        if (usuario != null)
        {
            _notify.NewNotification("Erro", ResourceErrorMessage.EMAIL_CADASTRADO);
            return;
        }
        
        var usuarioCommand = _mapper.Map<CadastrarUsuarioCommand>(dto);

        _mediator.Send(usuarioCommand);
    }

    private void ValidarCadastro(CadastrarUsuarioDto dto)
    {
        var validator = new CadastrarUsuarioDtoValidator();

        var result = validator.Validate(dto);
        
        GerarNotificationValidationResult(result);
    }

}