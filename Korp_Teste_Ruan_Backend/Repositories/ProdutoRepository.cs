namespace Korp_Teste_Ruan_Backend.Repositories;

using Microsoft.EntityFrameworkCore;
using Korp_Teste_Ruan_Backend.Data;
using Korp_Teste_Ruan_Backend.Models;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Produto>> GetAllAsync()
    {
        return await _context.Produtos
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Produto>> GetByEmpresaIdAsync(int empresaId)
    {
        return await _context.Produtos
            .AsNoTracking()
            .Where(p => p.EmpresaId == empresaId)
            .ToListAsync();
    }

    public async Task<Produto?> GetByIdAsync(int id)
    {
        return await _context.Produtos
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProdutoId == id);
    }

    public async Task<Produto?> GetByCodigoAsync(int empresaId, string codigo)
    {
        return await _context.Produtos
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.EmpresaId == empresaId && p.Codigo == codigo);
    }

    public async Task<Produto> AddAsync(Produto produto)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
        return produto;
    }

    public async Task<Produto?> UpdateAsync(Produto produto)
    {
        var existente = await _context.Produtos
            .FirstOrDefaultAsync(p => p.ProdutoId == produto.ProdutoId);

        if (existente is null)
            return null;

        existente.Codigo = produto.Codigo;
        existente.Descricao = produto.Descricao;
        existente.Saldo = produto.Saldo;

        // Concorrência otimista: informa ao EF a versão original recebida do cliente
        _context.Entry(existente).Property(p => p.RowVersion).OriginalValue = produto.RowVersion;

        await _context.SaveChangesAsync();
        return existente;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto is null)
            return false;

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Produtos.AnyAsync(p => p.ProdutoId == id);
    }

    public async Task<bool> EmpresaExistsAsync(int empresaId)
    {
        return await _context.Empresas.AnyAsync(e => e.EmpresaId == empresaId);
    }
}