using Application.Authorization.Dto;
using Application.Authorization.Dto.Validators;
using Domain.Authentication.Commands;
using Infra.CrossCutting.Util.Notifications.Resourcers;

namespace Application.Authorization.AppService;

public partial class AuthorizationAppService
{
    /// <summary>
    /// Utilizado para cadastrar um usuário no sistema
    /// </summary>
    /// <param name="dto">Dados necessários para o cadastro do usuário</param>
    public void CadastrarUsuario(CadastrarUsuarioDto dto)
    {
        Validar(dto);

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

    private void Validar(CadastrarUsuarioDto dto)
    {
        var validator = new CadastrarUsuarioDtoValidator();

        var result = validator.Validate(dto);
        
        if (!result.IsValid)
            foreach (var erros in result.Errors)
                _notify.NewNotification("Erro", erros.ErrorMessage);
    }

}