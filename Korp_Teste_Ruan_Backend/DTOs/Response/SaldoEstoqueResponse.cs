using Korp_Teste_Ruan_Backend.Models;

namespace Korp_Teste_Ruan_Backend.DTOs.Response;

public class SaldoEstoqueResponse
{
    public int TotalProdutos { get; set; }
    public decimal SaldoTotal { get; set; }
    public int ProdutosBaixoEstoque { get; set; }
    public int ProdutosSemEstoque { get; set; }
    public IEnumerable<Produto> Produtos { get; set; } = Array.Empty<Produto>();
}
