using System.ComponentModel.DataAnnotations;

namespace Korp_Teste_Ruan_Backend.DTOs.Request;

public class CriarProdutoRequest
{
    [Required(ErrorMessage = "O ID da Empresa é obrigatório.")]
    public int EmpresaId { get; set; }

    [Required(ErrorMessage = "Código do produto é obrigatório.")]
    [MaxLength(50)]
    public string Codigo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição é obrigatória.")]
    [MaxLength(500)]
    public string Descricao { get; set; } = string.Empty;

    [Range(0, double.MaxValue, ErrorMessage = "Saldo inicial deve ser >= 0.")]
    public decimal SaldoInicial { get; set; }
}
