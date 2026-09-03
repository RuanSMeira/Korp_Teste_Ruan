namespace Korp_Teste_Ruan_Backend.Controllers;

using Microsoft.AspNetCore.Mvc;
using Korp_Teste_Ruan_Backend.Models;
using Korp_Teste_Ruan_Backend.Services;
using Korp_Teste_Ruan_Backend.DTOs.Request;
using Korp_Teste_Ruan_Backend.DTOs.Response;

[ApiController]
[Route("api/notafiscal/{notaFiscalId:int}/itens")]
public class ItemNotaFiscalController : ControllerBase
{
    private readonly ItemNotaFiscalService _service;

    public ItemNotaFiscalController(ItemNotaFiscalService service)
    {
        _service = service;
    }

    // POST: api/notafiscal/1/itens
    [HttpPost]
    public async Task<ActionResult<ItemNotaFiscalResponse>> Create(int notaFiscalId, [FromBody] AdicionarItemRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var itemNota = new ItemNotaFiscal
            {
                NotaFiscalId = notaFiscalId,
                ProdutoId = request.ProdutoId,
                QuantidadeProduto = request.Quantidade
            };

            var criado = await _service.CreateAsync(itemNota);

            // ✅ mapeia pra DTO antes de retornar — sem isso, o ciclo Produto ↔ ItensNotaFiscal estoura de novo
            var response = new ItemNotaFiscalResponse
            {
                Id = criado.ItemId,
                ProdutoId = criado.ProdutoId,
                Quantidade = criado.QuantidadeProduto
            };

            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }
}