using Domain.Authentication.QueueEvents;
using FluentValidation;
using Infra.CrossCutting.Util.Notifications.Resourcers;

namespace Application.Queue.Events.Validators;

public class InserirUltimoLoginEventValidator : AbstractValidator<InserirUltimoLoginEvent>
{
    public InserirUltimoLoginEventValidator()
    {
        RuleFor(request => request.UsuarioId)
            .Must(req => !req.Equals(Guid.Empty))
            .WithMessage(ResourceErrorMessage.USUARIO_VAZIO);

        RuleFor(request => request.DataDoUltimoLogin)
            .Must(req => !req.Equals(DateTimeOffset.MinValue))
            .WithMessage(ResourceErrorMessage.DATA_MINIMA);
    }
}