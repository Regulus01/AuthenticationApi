using AutoMapper;
using Domain.Authentication.Commands;
using Domain.Authentication.Configuration;
using Domain.Authentication.Interface;
using Infra.CrossCutting.Util.Notifications.Interface;
using MediatR;

namespace Domain.Authentication.Handle;

public partial class AuthenticationCommandHandler : IRequestHandler<CadastrarUsuarioCommand>, 
                                                    IRequestHandler<LoginCommand, TokenModel>,
                                                    IRequestHandler<InserirUltimoLoginCommand>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;
    private readonly INotify _notify;

    public AuthenticationCommandHandler(IMapper mapper, IUsuarioRepository usuarioRepository,
                                        INotify notify)
    { 
        _mapper = mapper;
        _usuarioRepository = usuarioRepository;
        _notify = notify;
    }
}