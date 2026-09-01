using System.ComponentModel.DataAnnotations;

namespace Korp_Teste_Ruan_Backend.DTOs.Request;

public class LoginRequest
{
    [Required(ErrorMessage = "Email é obrigatório.")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha é obrigatória.")]
    public string Senha { get; set; } = string.Empty;
}
