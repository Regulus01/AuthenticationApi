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
            .WithMessage(ResourceErrorMessage.EMAIL_VAZIO)
            .Must(email => Regex.IsMatch(email, "^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$"))
            .WithMessage(ResourceErrorMessage.FORMATO_EMAIL_INVALIDO); 
        
        RuleFor(request => request.Password)
            .NotEmpty()
            .WithMessage(ResourceErrorMessage.SENHA_VAZIA); 
    }
}