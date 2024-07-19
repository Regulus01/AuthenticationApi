using System.ComponentModel.DataAnnotations;
using Infra.CrossCutting.Util.Notifications.Resourcers;

namespace Application.Authorization.Dto;

public class CadastrarUsuarioDto
{
    /// <summary>
    /// Email do usuário
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(ResourceErrorMessage),
              ErrorMessageResourceName = nameof(ResourceErrorMessage.EMAIL_VAZIO))]
    public string Email { get; set; } = "";

    /// <summary>
    /// Senha do usuário
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(ResourceErrorMessage),
              ErrorMessageResourceName = nameof(ResourceErrorMessage.SENHA_VAZIA))]
    public string Password { get; set; } = "";
}