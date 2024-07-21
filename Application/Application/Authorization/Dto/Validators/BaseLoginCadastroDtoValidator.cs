using FluentValidation;
using Infra.CrossCutting.Util.Notifications.Resourcers;

namespace Application.Authorization.Dto.Validators;

/// <summary>
/// Validador base para os validadores cadastro e login
/// </summary>
public abstract class BaseLoginCadastroDtoValidator : AbstractValidator<BaseLoginCadastroDto>
{
    protected BaseLoginCadastroDtoValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMessage.EMAIL_VAZIO)
            .EmailAddress()
            .WithMessage(ResourceErrorMessage.FORMATO_EMAIL_INVALIDO);

        RuleFor(request => request.Password)
            .NotEmpty()
            .WithMessage(ResourceErrorMessage.SENHA_VAZIA);
    }
}