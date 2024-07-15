using System.Text.RegularExpressions;
using Application.Interface;
using Application.ViewModels;
using AutoMapper;
using Domain.Authentication.Commands;
using Domain.Authentication.Interface;
using Infra.CrossCutting.Util.Notifications.Implementation;
using Infra.CrossCutting.Util.Notifications.Interface;
using MediatR;

namespace Application.Authorization.AppService;

public partial class AuthorizationAppService : IAuthorizationAppService
{
    private readonly Notify _notify;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IUsuarioRepository _usuarioRepository;

    public AuthorizationAppService(INotify notify, IMediator mediator, IMapper mapper, IUsuarioRepository usuarioRepository)
    {
        _mediator = mediator;
        _mapper = mapper;
        _usuarioRepository = usuarioRepository;
        _notify = notify.Invoke();
    }
    
    public TokenViewModel Login(LoginViewModel? message)
    {
        if (string.IsNullOrEmpty(message?.Email) || string.IsNullOrEmpty(message.Password))
        {
            _notify.NewNotification("Erro", "É necessário informar o email e senha");
            return new TokenViewModel();
        }
        
        if (!ValidarFormatoDoEmail(message.Email))
        {
            _notify.NewNotification("Erro", "Email invalido");
            return new TokenViewModel();
        }
        
        var loginCommand = _mapper.Map<LoginCommand>(message);

        var token = _mediator.Send(loginCommand).Result;

        return _mapper.Map<TokenViewModel>(token);
    }
    
    private bool ValidarFormatoDoEmail(string email)
    {
        return Regex.IsMatch(email, "^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\\\.[a-zA-Z0-9-.]+$");
    }
}