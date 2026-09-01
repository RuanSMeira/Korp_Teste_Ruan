namespace Korp_Teste_Ruan_Backend.Controllers;

using Microsoft.AspNetCore.Mvc;
using Korp_Teste_Ruan_Backend.Models;
using Korp_Teste_Ruan_Backend.Services;

[ApiController]
[Route("api/[controller]")]
public class EmpresaController : ControllerBase
{
    private readonly EmpresaService _service;

    public EmpresaController(EmpresaService service)
    {
        _service = service;
    }

    // GET: api/empresas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Empresa>>> GetAll()
    {
        var empresas = await _service.GetAllAsync();
        return Ok(empresas);
    }

    // GET: api/empresas/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Empresa>> GetById(int id)
    {
        var empresa = await _service.GetByIdAsync(id);

        if (empresa is null)
            return NotFound();

        return Ok(empresa);
    }

    // POST: api/empresas
    [HttpPost]
    public async Task<ActionResult<Empresa>> Create([FromBody] Empresa empresa)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var criada = await _service.CreateAsync(empresa);
            return CreatedAtAction(nameof(GetById), new { id = criada.EmpresaId }, criada);
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

    // PUT: api/empresas/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Empresa empresa)
    {
        try
        {
            var atualizada = await _service.UpdateAsync(id, empresa);

            if (atualizada is null)
                return NotFound();

            return Ok(atualizada);
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

    // DELETE: api/empresas/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var sucesso = await _service.DeleteAsync(id);

        if (!sucesso)
            return NotFound();

        return NoContent();
    }
}