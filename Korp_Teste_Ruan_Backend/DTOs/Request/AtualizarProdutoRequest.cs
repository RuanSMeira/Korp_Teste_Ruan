using System.ComponentModel.DataAnnotations;

namespace Korp_Teste_Ruan_Backend.DTOs.Request;

public class AtualizarProdutoRequest
{
    [Required(ErrorMessage = "Descrição é obrigatória.")]
    [MaxLength(500)]
    public string Descricao { get; set; } = string.Empty;

    [Range(0, double.MaxValue, ErrorMessage = "Saldo deve ser >= 0.")]
    public decimal Saldo { get; set; }
}
