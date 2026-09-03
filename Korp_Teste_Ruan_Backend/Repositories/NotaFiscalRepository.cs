namespace Korp_Teste_Ruan_Backend.Repositories;

using Microsoft.EntityFrameworkCore;
using Korp_Teste_Ruan_Backend.Data;
using Korp_Teste_Ruan_Backend.Models;

public class NotaFiscalRepository : INotaFiscalRepository
{
    private readonly AppDbContext _context;

    public NotaFiscalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<NotaFiscal>> GetAllAsync()
    {
        return await _context.NotasFiscais
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<NotaFiscal>> GetAllByEmpresaAsync(int empresaId)
    {
        return await _context.NotasFiscais
            .AsNoTracking()
            .Include(n => n.Itens)
            .Where(n => n.EmpresaId == empresaId)
            .OrderByDescending(n => n.NumeroSequencial)
            .ToListAsync();
    }

    public async Task<NotaFiscal?> GetByIdAsync(int id)
    {
        return await _context.NotasFiscais
            .AsNoTracking()
            .FirstOrDefaultAsync(n => n.NotaFiscalId == id);
    }

    public async Task<NotaFiscal?> GetByIdComItensAsync(int id)
    {
        return await _context.NotasFiscais
            .AsNoTracking()
            .Include(n => n.Itens)
                .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(n => n.NotaFiscalId == id);
    }

    public async Task<NotaFiscal> AddAsync(NotaFiscal notaFiscal)
    {
        _context.NotasFiscais.Add(notaFiscal);
        await _context.SaveChangesAsync();
        return notaFiscal;
    }

    public async Task<bool> EmpresaExistsAsync(int empresaId)
    {
        return await _context.Empresas.AnyAsync(e => e.EmpresaId == empresaId);
    }

    public async Task<bool> UsuarioExistsAsync(int usuarioId)
    {
        return await _context.Usuarios.AnyAsync(u => u.UsuarioId == usuarioId);
    }

    public async Task<int> GetProximoNumeroSequencialAsync(int empresaId)
    {
        var ultimoNumero = await _context.NotasFiscais
            .Where(n => n.EmpresaId == empresaId)
            .Select(n => (int?)n.NumeroSequencial)
            .MaxAsync();

        return (ultimoNumero ?? 0) + 1;
    }

    public async Task<NotaFiscal> UpdateAsync(NotaFiscal notaFiscal)
    {
        _context.NotasFiscais.Update(notaFiscal);
        await _context.SaveChangesAsync();
        return notaFiscal;
    }
}