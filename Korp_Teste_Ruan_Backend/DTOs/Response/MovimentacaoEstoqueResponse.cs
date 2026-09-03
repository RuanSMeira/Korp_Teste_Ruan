using Korp_Teste_Ruan_Backend.Models.Enums;

namespace Korp_Teste_Ruan_Backend.DTOs.Response;

public class MovimentacaoEstoqueResponse
{
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int ProdutoId { get; set; }
    public string Produto { get; set; } = string.Empty;
    public string CodigoProduto { get; set; } = string.Empty;
    public int UsuarioId { get; set; }
    public string Responsavel { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public decimal Quantidade { get; set; }
    public decimal SaldoAnterior { get; set; }
    public decimal SaldoPosterior { get; set; }
    public DateTime DataMovimentacao { get; set; }
    public string Observacao { get; set; } = string.Empty;
}