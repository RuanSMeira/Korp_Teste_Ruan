namespace Korp_Teste_Ruan_Backend.Repositories;

using Microsoft.EntityFrameworkCore;
using Korp_Teste_Ruan_Backend.Data;
using Korp_Teste_Ruan_Backend.Models;

public class EmpresaRepository : IEmpresaRepository
{
    private readonly AppDbContext _context;

    public EmpresaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Empresa>> GetAllAsync()
    {
        return await _context.Empresas
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Empresa?> GetByIdAsync(int id)
    {
        return await _context.Empresas
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EmpresaId == id);
    }

    public async Task<Empresa?> GetByCnpjAsync(string cnpj)
    {
        var cnpjNormalizado = new string(cnpj.Where(char.IsDigit).ToArray());
        return await _context.Empresas
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Cnpj == cnpjNormalizado);
    }

    public async Task<Empresa> AddAsync(Empresa empresa)
    {
        _context.Empresas.Add(empresa);
        await _context.SaveChangesAsync();
        return empresa;
    }

    public async Task<Empresa?> UpdateAsync(Empresa empresa)
    {
        var existente = await _context.Empresas
            .FirstOrDefaultAsync(e => e.EmpresaId == empresa.EmpresaId);

        if (existente is null)
            return null;

        existente.RazaoSocial = empresa.RazaoSocial;
        existente.NomeFantasia = empresa.NomeFantasia;
        existente.Cnpj = empresa.Cnpj;

        await _context.SaveChangesAsync();
        return existente;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var empresa = await _context.Empresas.FindAsync(id);
        if (empresa is null)
            return false;

        _context.Empresas.Remove(empresa);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Empresas.AnyAsync(e => e.EmpresaId == id);
    }
}
