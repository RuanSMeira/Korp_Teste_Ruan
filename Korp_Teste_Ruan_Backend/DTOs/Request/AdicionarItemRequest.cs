using System.ComponentModel.DataAnnotations;

namespace Korp_Teste_Ruan_Backend.DTOs.Request;

public class AdicionarItemRequest
{
    [Required]
    public int ProdutoId { get; set; }

    [Required]
    [Range(0.0001, double.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero.")]
    public decimal Quantidade { get; set; }
}
