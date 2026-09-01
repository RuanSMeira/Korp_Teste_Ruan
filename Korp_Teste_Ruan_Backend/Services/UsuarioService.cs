namespace Korp_Teste_Ruan_Backend.Services;

using Korp_Teste_Ruan_Backend.Models;
using Korp_Teste_Ruan_Backend.Interfaces;

public class UsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IEnumerable<Usuario>> GetByEmpresaIdAsync(int empresaId)
    {
        return await _repository.GetByEmpresaIdAsync(empresaId);
    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Usuario> CreateAsync(Usuario usuario)
    {
        await ValidarAsync(usuario);

        var existente = await _repository.GetByEmailAsync(usuario.Email);
        if (existente is not null)
            throw new InvalidOperationException($"Já existe um usuário cadastrado com o e-mail '{usuario.Email}'.");

        if (string.IsNullOrWhiteSpace(usuario.SenhaHash))
            throw new ArgumentException("A senha é obrigatória.");

        return await _repository.AddAsync(usuario);
    }

    public async Task<Usuario?> UpdateAsync(int id, Usuario usuario)
    {
        if (id != usuario.UsuarioId)
            throw new ArgumentException("O ID informado não corresponde ao ID do usuário.");

        await ValidarAsync(usuario);

        var existeOutroComMesmoEmail = await _repository.GetByEmailAsync(usuario.Email);
        if (existeOutroComMesmoEmail is not null && existeOutroComMesmoEmail.UsuarioId != id)
            throw new InvalidOperationException($"Já existe outro usuário cadastrado com o e-mail '{usuario.Email}'.");

        return await _repository.UpdateAsync(usuario);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    private async Task ValidarAsync(Usuario usuario)
    {
        if (string.IsNullOrWhiteSpace(usuario.NomeUsuario))
            throw new ArgumentException("O nome do usuário é obrigatório.");

        if (string.IsNullOrWhiteSpace(usuario.Email))
            throw new ArgumentException("O e-mail é obrigatório.");

        if (!usuario.Email.Contains('@'))
            throw new ArgumentException("O e-mail informado é inválido.");

        var empresaExiste = await _repository.EmpresaExistsAsync(usuario.EmpresaId);
        if (!empresaExiste)
            throw new ArgumentException($"Não existe empresa com o ID '{usuario.EmpresaId}'.");
    }
}