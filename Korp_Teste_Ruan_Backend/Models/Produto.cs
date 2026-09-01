namespace Korp_Teste_Ruan_Backend.Models;

/// <summary>
/// Entidade que representa um produto cadastrado por uma empresa.
/// </summary>
public class Produto
{
    public int ProdutoId { get; set; }
    public int EmpresaId { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Saldo { get; set; }
    public byte[] RowVersion { get; set; } = null!;

    // Navegação
    public Empresa Empresa { get; set; } = null!;
    public ICollection<ItemNotaFiscal> ItensNotaFiscal { get; set; } = new List<ItemNotaFiscal>();
}
