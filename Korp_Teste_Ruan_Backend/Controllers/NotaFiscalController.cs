namespace Korp_Teste_Ruan_Backend.Controllers;

using Korp_Teste_Ruan_Backend.DTOs.Request;
using Korp_Teste_Ruan_Backend.Services;
using Korp_Teste_Ruan_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class NotaFiscalController : ControllerBase
{
    private readonly NotaFiscalService _notaFiscalService;

    public NotaFiscalController(NotaFiscalService notaFiscalService)
    {
        _notaFiscalService = notaFiscalService;
    }

    // POST: api/notafiscal
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CriarNotaFiscalRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var nota = await _notaFiscalService.CreateComItensAsync(request);

            // Retorna o DTO de resposta criado no seu Service
            return Ok(nota);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { erro = ex.Message });
        }
    }

    // GET: api/notafiscal/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var notaFiscal = await _notaFiscalService.GetByIdAsync(id);

        if (notaFiscal == null)
            return NotFound(new { mensagem = $"Nota fiscal com ID {id} não encontrada." });

        return Ok(notaFiscal);
    }

    // PUT: api/notafiscal/5/emitir
    [HttpPut("{id:int}/emitir")]
    public async Task<IActionResult> Emitir(int id)
    {
        try
        {
            var notaEmitida = await _notaFiscalService.EmitirNotaAsync(id);
            return Ok(new { mensagem = "Nota fiscal emitida com sucesso!", nota = notaEmitida });
        }
        catch (ArgumentException ex)
        {
            // Retorna 404 se a nota ou produto não existirem
            return NotFound(new { erro = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            // Retorna 400 se já estiver fechada ou se não houver saldo no estoque
            return BadRequest(new { erro = ex.Message });
        }
    }
}