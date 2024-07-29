using Application.Authorization.Interface;
using AutoMapper;
using Domain.Authentication.Interface;
using FluentValidation.Results;
using Infra.CrossCutting.Util.Notifications.Interface;
using MediatR;
using PublisherBus.Bus;

namespace Application.Authorization.AppService;

public partial class AuthorizationAppService : IAuthorizationAppService
{
    private readonly INotify _notify;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IUsuarioRepository _repository;

    public AuthorizationAppService(INotify notify, IMediator mediator, IMapper mapper, IUsuarioRepository repository, IPublishBus bus)
    {
        _mediator = mediator;
        _mapper = mapper;
        _repository = repository;
        _notify = notify;
    }
    
    private void GerarNotificationValidationResult(ValidationResult result)
    {
        if (!result.IsValid)
            foreach (var erros in result.Errors)
                _notify.NewNotification("Erro", erros.ErrorMessage);
    }
}