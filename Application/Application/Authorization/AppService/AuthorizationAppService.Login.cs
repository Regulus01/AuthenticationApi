using Application.Authorization.Dto;
using Application.Authorization.Dto.Validators;
using Application.ViewModels;
using Domain.Authentication.Commands;

namespace Application.Authorization.AppService;

public partial class AuthorizationAppService
{
    /// <inheritdoc />
    public TokenViewModel Login(LoginDto dto)
    {
        ValidarLogin(dto);

        if (_notify.HasNotifications())
            return new TokenViewModel();
        
        var loginCommand = _mapper.Map<LoginCommand>(dto);

        var token = _mediator.Send(loginCommand).Result;

        return _mapper.Map<TokenViewModel>(token);
    }

    public void InserirUltimoLogin(Guid usuarioId, InserirUltimoLoginDto dto)
    {
        var command = _mapper.Map<InserirUltimoLoginCommand>(dto, opt => opt.Items.Add("UsuarioId", usuarioId));

        _mediator.Send(command);
    }
    
    private void ValidarLogin(LoginDto dto)
    {
        var validador = new LoginDtoValidator();
        
        var result = validador.Validate(dto);
        
        GerarNotificationValidationResult(result);
    }
}