using System.ComponentModel.DataAnnotations;

namespace Application.Authorization.ViewModels;

public class LoginDto
{
    /// <summary>
    /// Email do usuário
    /// </summary>
    [Required]
    public string? Email { get; set; }
    /// <summary>
    /// Senha do usuário
    /// </summary>
    [Required]
    public string? Password { get; set; }
}