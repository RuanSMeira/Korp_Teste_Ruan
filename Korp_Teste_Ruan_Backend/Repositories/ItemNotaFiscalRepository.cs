namespace Korp_Teste_Ruan_Backend.Repositories;

using Microsoft.EntityFrameworkCore;
using Korp_Teste_Ruan_Backend.Data;
using Korp_Teste_Ruan_Backend.Models;

public class ItemNotaFiscalRepository : IItemNotaFiscalRepository
{
    private readonly AppDbContext _context;

    public ItemNotaFiscalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ItemNotaFiscal>> GetAllAsync()
    {
        return await _context.ItensNotaFiscal
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<ItemNotaFiscal>> GetByNotaFiscalIdAsync(int notaFiscalId)
    {
        return await _context.ItensNotaFiscal
            .AsNoTracking()
            .Where(i => i.NotaFiscalId == notaFiscalId)
            .ToListAsync();
    }

    public async Task<ItemNotaFiscal?> GetByIdAsync(int id)
    {
        return await _context.ItensNotaFiscal
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.ItemId == id);
    }

    public async Task<ItemNotaFiscal> AddAsync(ItemNotaFiscal item)
    {
        _context.ItensNotaFiscal.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<ItemNotaFiscal?> UpdateAsync(ItemNotaFiscal item)
    {
        var existente = await _context.ItensNotaFiscal
            .FirstOrDefaultAsync(i => i.ItemId == item.ItemId);

        if (existente is null)
            return null;

        existente.ProdutoId = item.ProdutoId;
        existente.QuantidadeProduto = item.QuantidadeProduto;

        await _context.SaveChangesAsync();
        return existente;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _context.ItensNotaFiscal.FindAsync(id);
        if (item is null)
            return false;

        _context.ItensNotaFiscal.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.ItensNotaFiscal.AnyAsync(i => i.ItemId == id);
    }

    public async Task<bool> NotaFiscalExistsAsync(int notaFiscalId)
    {
        return await _context.NotasFiscais.AnyAsync(n => n.NotaFiscalId == notaFiscalId);
    }

    public async Task<Produto?> GetProdutoAsync(int produtoId)
    {
        return await _context.Produtos
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProdutoId == produtoId);
    }
}