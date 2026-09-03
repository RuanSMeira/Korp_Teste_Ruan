using Korp_Teste_Ruan_Backend.DTOs.Request;
using Korp_Teste_Ruan_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Korp_Teste_Ruan_Backend.Controllers;

[ApiController]
[Route("api/movimentacoesestoque")]
public class MovimentacoesEstoqueController : ControllerBase
{
    private readonly MovimentacaoEstoqueService _service;
    public MovimentacoesEstoqueController(MovimentacaoEstoqueService service) => _service = service;

    [HttpGet("empresa/{empresaId:int}")]
    public async Task<IActionResult> Listar(int empresaId) => Ok(await _service.ListarPorEmpresaAsync(empresaId));

    [HttpPost]
    public async Task<IActionResult> Criar(CriarMovimentacaoEstoqueRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try { return Ok(await _service.CriarAsync(request)); }
        catch (ArgumentException ex) { return BadRequest(new { erro = ex.Message }); }
        catch (InvalidOperationException ex) { return BadRequest(new { erro = ex.Message }); }
    }
}