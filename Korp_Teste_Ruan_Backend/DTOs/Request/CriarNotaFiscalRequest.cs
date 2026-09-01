using System.ComponentModel.DataAnnotations;

namespace Korp_Teste_Ruan_Backend.DTOs.Request;

public class CriarNotaFiscalRequest
{
    [Required(ErrorMessage = "A nota fiscal deve ter ao menos um item.")]
    [MinLength(1, ErrorMessage = "A nota fiscal deve ter ao menos um item.")]
    public List<ItemNotaFiscalRequest> Itens { get; set; } = new();
}

public class ItemNotaFiscalRequest
{
    [Required]
    public int ProdutoId { get; set; }

    [Required]
    [Range(0.0001, double.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero.")]
    public decimal Quantidade { get; set; }
}
