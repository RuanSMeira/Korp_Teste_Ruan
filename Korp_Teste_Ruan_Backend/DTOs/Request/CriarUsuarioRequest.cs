using System.ComponentModel.DataAnnotations;

namespace Korp_Teste_Ruan_Backend.DTOs.Request;

public class CriarUsuarioRequest
{
    [Required(ErrorMessage = "Nome do usuário é obrigatório.")]
    [MaxLength(150)]
    public string NomeUsuario { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    [MaxLength(250)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha é obrigatória.")]
    [MinLength(8, ErrorMessage = "Senha deve ter no mínimo 8 caracteres.")]
    public string Senha { get; set; } = string.Empty;

    [Required(ErrorMessage = "ID da empresa é obrigatório.")]
    public int EmpresaId { get; set; }
}
