using System.ComponentModel.DataAnnotations;

namespace Application.Authorization.Dto;

public class CadastrarUsuarioDto
{
    
    /// <summary>
    /// Email do usuário
    /// </summary>
    [Required(ErrorMessage = "O email é obrigatório")]
    public string Email { get; set; } = "";
    
    /// <summary>
    /// Senha do usuário
    /// </summary>
    [Required(ErrorMessage = "A senha é obrigatório")]
    public string Password { get; set; } = "";

}