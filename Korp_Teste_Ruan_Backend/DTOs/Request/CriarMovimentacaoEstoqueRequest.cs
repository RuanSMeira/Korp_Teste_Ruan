using System.ComponentModel.DataAnnotations;
using Korp_Teste_Ruan_Backend.Models.Enums;

namespace Korp_Teste_Ruan_Backend.DTOs.Request;

public class CriarMovimentacaoEstoqueRequest
{
    [Range(1, int.MaxValue)] public int EmpresaId { get; set; }
    [Range(1, int.MaxValue)] public int ProdutoId { get; set; }
    [Range(1, int.MaxValue)] public int UsuarioId { get; set; }
    public TipoMovimentacaoEstoque Tipo { get; set; }
    [Range(0.0001, double.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    public decimal Quantidade { get; set; }
    [MaxLength(500)] public string Observacao { get; set; } = string.Empty;
}