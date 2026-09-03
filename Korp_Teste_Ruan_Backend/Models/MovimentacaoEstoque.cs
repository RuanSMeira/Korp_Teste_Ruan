using Korp_Teste_Ruan_Backend.Models.Enums;

namespace Korp_Teste_Ruan_Backend.Models;

public class MovimentacaoEstoque
{
    public int MovimentacaoEstoqueId { get; set; }
    public int EmpresaId { get; set; }
    public int ProdutoId { get; set; }
    public int UsuarioId { get; set; }
    public TipoMovimentacaoEstoque Tipo { get; set; }
    public decimal Quantidade { get; set; }
    public decimal SaldoAnterior { get; set; }
    public decimal SaldoPosterior { get; set; }
    public DateTime DataMovimentacao { get; set; } = DateTime.UtcNow;
    public string Observacao { get; set; } = string.Empty;

    public Empresa Empresa { get; set; } = null!;
    public Produto Produto { get; set; } = null!;
    public Usuario Usuario { get; set; } = null!;
}