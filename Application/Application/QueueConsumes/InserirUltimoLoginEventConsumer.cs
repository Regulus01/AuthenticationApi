using Application.Queue.Events.Validators;
using AutoMapper;
using Domain.Authentication.Commands;
using Domain.Authentication.QueueEvents;
using Infra.CrossCutting.Util.Notifications.Interface;
using MassTransit;
using MediatR;

namespace Application.QueueConsumes;

public class InserirUltimoLoginEventConsumer : IConsumer<InserirUltimoLoginEvent>
{
    private readonly INotify _notify;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<InserirUltimoLoginEventConsumer> _logger;

    public InserirUltimoLoginEventConsumer(INotify notify, IMediator mediator, IMapper mapper,
        ILogger<InserirUltimoLoginEventConsumer> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
        _notify = notify;
    }

    public async Task Consume(ConsumeContext<InserirUltimoLoginEvent> context)
    {
        var message = context.Message;

        Validar(message);

        if (_notify.HasNotifications())
            return;

        var command = _mapper.Map<InserirUltimoLoginCommand>(message);

        await _mediator.Send(command);
    }

    private void Validar(InserirUltimoLoginEvent evento)
    {
        var validator = new InserirUltimoLoginEventValidator();

        var result = validator.Validate(evento);

        if (!result.IsValid)
            foreach (var erros in result.Errors)
            {
                _notify.NewNotification("Erro", erros.ErrorMessage);
                _logger.LogError(erros.ErrorMessage);
            }
    }
}