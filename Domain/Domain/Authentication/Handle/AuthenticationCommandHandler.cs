using AutoMapper;
using Domain.Authentication.Commands;
using Domain.Authentication.Configuration;
using Domain.Authentication.Interface;
using Infra.CrossCutting.Util.Notifications.Interface;
using MediatR;
using PublisherBus.Bus;

namespace Domain.Authentication.Handle;

public partial class AuthenticationCommandHandler : IRequestHandler<CadastrarUsuarioCommand>, 
                                                    IRequestHandler<LoginCommand, TokenModel>,
                                                    IRequestHandler<InserirUltimoLoginCommand>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;
    private readonly INotify _notify;
    private readonly IPublishBus _bus;

    public AuthenticationCommandHandler(IMapper mapper, IUsuarioRepository usuarioRepository,
                                        INotify notify, IPublishBus bus)
    { 
        _mapper = mapper;
        _usuarioRepository = usuarioRepository;
        _notify = notify;
        _bus = bus;
    }
}