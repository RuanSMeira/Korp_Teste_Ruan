namespace Korp_Teste_Ruan_Backend.Repositories;

using Korp_Teste_Ruan_Backend.Models;

public interface IItemNotaFiscalRepository
{
    Task<IEnumerable<ItemNotaFiscal>> GetAllAsync();
    Task<IEnumerable<ItemNotaFiscal>> GetByNotaFiscalIdAsync(int notaFiscalId);
    Task<ItemNotaFiscal?> GetByIdAsync(int id);
    Task<ItemNotaFiscal> AddAsync(ItemNotaFiscal item);
    Task<ItemNotaFiscal?> UpdateAsync(ItemNotaFiscal item);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> NotaFiscalExistsAsync(int notaFiscalId);
    Task<Produto?> GetProdutoAsync(int produtoId);
}