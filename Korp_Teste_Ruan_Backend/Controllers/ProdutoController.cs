namespace Korp_Teste_Ruan_Backend.Controllers;

using Korp_Teste_Ruan_Backend.DTOs.Request;
using Korp_Teste_Ruan_Backend.Models;
using Korp_Teste_Ruan_Backend.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoService _service;

    public ProdutosController(ProdutoService service)
    {
        _service = service;
    }

    // GET: api/produtos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetAll()
    {
        var produtos = await _service.GetAllAsync();
        return Ok(produtos);
    }

    // GET: api/produtos/empresa/3
    [HttpGet("empresa/{empresaId:int}")]
    public async Task<ActionResult<IEnumerable<Produto>>> GetByEmpresa(int empresaId)
    {
        var produtos = await _service.GetByEmpresaIdAsync(empresaId);
        return Ok(produtos);
    }

    // GET: api/produtos/empresa/3/saldo
    [HttpGet("empresa/{empresaId:int}/saldo")]
    public async Task<IActionResult> GetSaldoByEmpresa(int empresaId)
    {
        return Ok(await _service.GetSaldoByEmpresaIdAsync(empresaId));
    }

    // GET: api/produtos/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Produto>> GetById(int id)
    {
        var produto = await _service.GetByIdAsync(id);

        if (produto is null)
            return NotFound();

        return Ok(produto);
    }

    // POST: api/produtos
    [HttpPost]
    public async Task<ActionResult<Produto>> Create([FromBody] CriarProdutoRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            // Mapeando os dados do DTO para a entidade Produto
            var produto = new Produto
            {
                EmpresaId = request.EmpresaId, // Linha adicionada
                Codigo = request.Codigo,
                Descricao = request.Descricao,
                Saldo = request.SaldoInicial // (ou Saldo, conforme nomeado na sua entidade)
            };

            var criado = await _service.CreateAsync(produto);
            return CreatedAtAction(nameof(GetById), new { id = criado.ProdutoId }, criado);
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

    // PUT: api/produtos/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Produto produto)
    {
        try
        {
            var atualizado = await _service.UpdateAsync(id, produto);

            if (atualizado is null)
                return NotFound();

            return Ok(atualizado);
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

    // DELETE: api/produtos/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var sucesso = await _service.DeleteAsync(id);

        if (!sucesso)
            return NotFound();

        return NoContent();
    }
}