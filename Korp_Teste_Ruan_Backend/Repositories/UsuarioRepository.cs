namespace Korp_Teste_Ruan_Backend.Repositories;

using Microsoft.EntityFrameworkCore;
using Korp_Teste_Ruan_Backend.Data;
using Korp_Teste_Ruan_Backend.Models;
using Korp_Teste_Ruan_Backend.Interfaces;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return await _context.Usuarios
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Usuario>> GetByEmpresaIdAsync(int empresaId)
    {
        return await _context.Usuarios
            .AsNoTracking()
            .Where(u => u.EmpresaId == empresaId)
            .ToListAsync();
    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        return await _context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UsuarioId == id);
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _context.Usuarios
            .AsNoTracking()
            .Include(u => u.Empresa)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Usuario> AddAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<Usuario?> UpdateAsync(Usuario usuario)
    {
        var existente = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.UsuarioId == usuario.UsuarioId);

        if (existente is null)
            return null;

        existente.NomeUsuario = usuario.NomeUsuario;
        existente.Email = usuario.Email;

        // SenhaHash só é atualizada se foi enviada uma nova
        if (!string.IsNullOrWhiteSpace(usuario.SenhaHash))
            existente.SenhaHash = usuario.SenhaHash;

        await _context.SaveChangesAsync();
        return existente;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario is null)
            return false;

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Usuarios.AnyAsync(u => u.UsuarioId == id);
    }

    public async Task<bool> EmpresaExistsAsync(int empresaId)
    {
        return await _context.Empresas.AnyAsync(e => e.EmpresaId == empresaId);
    }
}