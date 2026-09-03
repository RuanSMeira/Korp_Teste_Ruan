namespace Korp_Teste_Ruan_Backend.Controllers;

using Microsoft.AspNetCore.Mvc;
using Korp_Teste_Ruan_Backend.Models;
using Korp_Teste_Ruan_Backend.Services;
using Korp_Teste_Ruan_Backend.DTOs.Request;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioService _service;

    public UsuarioController(UsuarioService service)
    {
        _service = service;
    }

    // POST: api/usuario/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var usuario = await _service.LoginAsync(request);

        if (usuario is null)
            return Unauthorized(new { erro = "E-mail ou senha inválidos." });

        return Ok(new
        {
            usuarioId = usuario.UsuarioId,
            empresaId = usuario.EmpresaId,
            nomeUsuario = usuario.NomeUsuario,
            email = usuario.Email,
            nomeFantasia = usuario.Empresa?.NomeFantasia,
            cnpj = usuario.Empresa?.Cnpj,
            perfil = "usuario"
        });
    }

    // GET: api/usuario
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
    {
        var usuarios = await _service.GetAllAsync();
        return Ok(usuarios);
    }

    // GET: api/usuario/empresa/3
    [HttpGet("empresa/{empresaId:int}")]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetByEmpresa(int empresaId)
    {
        var usuarios = await _service.GetByEmpresaIdAsync(empresaId);
        return Ok(usuarios);
    }

    // GET: api/usuario/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Usuario>> GetById(int id)
    {
        var usuario = await _service.GetByIdAsync(id);

        if (usuario is null)
            return NotFound();

        return Ok(usuario);
    }

    // POST: api/usuario
    [HttpPost]
    public async Task<ActionResult<Usuario>> Create([FromBody] CriarUsuarioRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var usuario = new Usuario
            {
                EmpresaId = request.EmpresaId,
                NomeUsuario = request.NomeUsuario.Trim(),
                Email = request.Email.Trim().ToLowerInvariant(),
                SenhaHash = request.Senha
            };
            var criado = await _service.CreateAsync(usuario);
            return CreatedAtAction(nameof(GetById), new { id = criado.UsuarioId }, criado);
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

    // PUT: api/usuario/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Usuario usuario)
    {
        try
        {
            var atualizado = await _service.UpdateAsync(id, usuario);

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

    // DELETE: api/usuario/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var sucesso = await _service.DeleteAsync(id);

        if (!sucesso)
            return NotFound();

        return NoContent();
    }
}
