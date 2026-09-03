using System.ComponentModel.DataAnnotations;

namespace Korp_Teste_Ruan_Backend.DTOs.Request;

public class LoginEmpresaRequest
{
    [Required(ErrorMessage = "CNPJ é obrigatório.")]
    public string Cnpj { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha master é obrigatória.")]
    public string Senha { get; set; } = string.Empty;
}
