using System.ComponentModel.DataAnnotations;

namespace Korp_Teste_Ruan_Backend.DTOs.Request;

public class CriarEmpresaRequest
{
    [Required(ErrorMessage = "Razão social é obrigatória.")]
    [MaxLength(200)]
    public string RazaoSocial { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nome fantasia é obrigatório.")]
    [MaxLength(200)]
    public string NomeFantasia { get; set; } = string.Empty;

    [Required(ErrorMessage = "CNPJ é obrigatório.")]
    [StringLength(14, MinimumLength = 14, ErrorMessage = "CNPJ deve ter 14 dígitos.")]
    public string Cnpj { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha master é obrigatória.")]
    [MinLength(8, ErrorMessage = "Senha master deve ter no mínimo 8 caracteres.")]
    public string SenhaMaster { get; set; } = string.Empty;
}
