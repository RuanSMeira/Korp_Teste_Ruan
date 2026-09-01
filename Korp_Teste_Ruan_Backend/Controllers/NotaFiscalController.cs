namespace Korp_Teste_Ruan_Backend.Controllers;

using Microsoft.AspNetCore.Mvc;
using Korp_Teste_Ruan_Backend.Models;
using Korp_Teste_Ruan_Backend.Services;

[ApiController]
[Route("api/[controller]")]
public class ItensNotaFiscalController : ControllerBase
{
    private readonly ItemNotaFiscalService _service;

    public ItensNotaFiscalController(ItemNotaFiscalService service)
    {
        _service = service;
    }

    // GET: api/itensnotafiscal
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemNotaFiscal>>> GetAll()
    {
        var itens = await _service.GetAllAsync();
        return Ok(itens);
    }

    // GET: api/itensnotafiscal/notafiscal/7
    [HttpGet("notafiscal/{notaFiscalId:int}")]
    public async Task<ActionResult<IEnumerable<ItemNotaFiscal>>> GetByNotaFiscal(int notaFiscalId)
    {
        var itens = await _service.GetByNotaFiscalIdAsync(notaFiscalId);
        return Ok(itens);
    }

    // GET: api/itensnotafiscal/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ItemNotaFiscal>> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);

        if (item is null)
            return NotFound();

        return Ok(item);
    }

    // POST: api/itensnotafiscal
    [HttpPost]
    public async Task<ActionResult<ItemNotaFiscal>> Create([FromBody] ItemNotaFiscal item)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var criado = await _service.CreateAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = criado.ItemId }, criado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/itensnotafiscal/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ItemNotaFiscal item)
    {
        try
        {
            var atualizado = await _service.UpdateAsync(id, item);

            if (atualizado is null)
                return NotFound();

            return Ok(atualizado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/itensnotafiscal/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var sucesso = await _service.DeleteAsync(id);

        if (!sucesso)
            return NotFound();

        return NoContent();
    }
}