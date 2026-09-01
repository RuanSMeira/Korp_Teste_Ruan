namespace Korp_Teste_Ruan_Backend.Services;

using Korp_Teste_Ruan_Backend.Models;
using Korp_Teste_Ruan_Backend.Repositories;

public class ItemNotaFiscalService
{
    private readonly IItemNotaFiscalRepository _repository;

    public ItemNotaFiscalService(IItemNotaFiscalRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ItemNotaFiscal>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IEnumerable<ItemNotaFiscal>> GetByNotaFiscalIdAsync(int notaFiscalId)
    {
        return await _repository.GetByNotaFiscalIdAsync(notaFiscalId);
    }

    public async Task<ItemNotaFiscal?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<ItemNotaFiscal> CreateAsync(ItemNotaFiscal item)
    {
        await ValidarAsync(item);
        return await _repository.AddAsync(item);
    }

    public async Task<ItemNotaFiscal?> UpdateAsync(int id, ItemNotaFiscal item)
    {
        if (id != item.ItemId)
            throw new ArgumentException("O ID informado não corresponde ao ID do item.");

        await ValidarAsync(item);

        return await _repository.UpdateAsync(item);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    private async Task ValidarAsync(ItemNotaFiscal item)
    {
        if (item.QuantidadeProduto <= 0)
            throw new ArgumentException("A quantidade do produto deve ser maior que zero.");

        var notaFiscalExiste = await _repository.NotaFiscalExistsAsync(item.NotaFiscalId);
        if (!notaFiscalExiste)
            throw new ArgumentException($"Não existe nota fiscal com o ID '{item.NotaFiscalId}'.");

        var produto = await _repository.GetProdutoAsync(item.ProdutoId);
        if (produto is null)
            throw new ArgumentException($"Não existe produto com o ID '{item.ProdutoId}'.");
    }
}