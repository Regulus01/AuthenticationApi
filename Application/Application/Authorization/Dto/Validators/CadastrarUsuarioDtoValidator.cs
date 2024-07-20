using System.Text.RegularExpressions;
using FluentValidation;
using Infra.CrossCutting.Util.Notifications.Resourcers;

namespace Application.Authorization.Dto.Validators;

public class CadastrarUsuarioDtoValidator : AbstractValidator<CadastrarUsuarioDto>
{
    public CadastrarUsuarioDtoValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .WithMessage(GetErrorMessage(ResourceErrorMessage.EMAIL_VAZIO))
            .Must(email => Regex.IsMatch(email, "^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$"))
            .WithMessage(GetErrorMessage(ResourceErrorMessage.FORMATO_EMAIL_INVALIDO));

        RuleFor(request => request.Password)
            .NotEmpty()
            .WithMessage(GetErrorMessage(ResourceErrorMessage.SENHA_VAZIA));
    }

    private string GetErrorMessage(string resourceMessage)
    {
        Console.WriteLine($"Loading error message: {resourceMessage}");
        return resourceMessage ?? "Default error message";
    }
}