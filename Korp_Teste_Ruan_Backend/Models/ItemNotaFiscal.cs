namespace Korp_Teste_Ruan_Backend.Models;

/// <summary>
/// Entidade que representa um item de uma nota fiscal.
/// </summary>
public class ItemNotaFiscal
{
    public int ItemId { get; set; }
    public int NotaFiscalId { get; set; }
    public int ProdutoId { get; set; }
    public decimal QuantidadeProduto { get; set; }

    // Navegação
    public NotaFiscal NotaFiscal { get; set; } = null!;
    public Produto Produto { get; set; } = null!;
}
