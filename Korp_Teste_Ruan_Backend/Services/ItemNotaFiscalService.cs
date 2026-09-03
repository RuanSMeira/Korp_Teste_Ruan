namespace Korp_Teste_Ruan_Backend.Services;

using Korp_Teste_Ruan_Backend.Models;
using Korp_Teste_Ruan_Backend.Repositories;
using Korp_Teste_Ruan_Backend.Services.Interfaces;

public class ItemNotaFiscalService : IItemNotaFiscalService
{
    private readonly IItemNotaFiscalRepository _itemRepository;
    private readonly IProdutoRepository _produtoRepository;

    public ItemNotaFiscalService(
        IItemNotaFiscalRepository itemRepository,
        IProdutoRepository produtoRepository)
    {
        _itemRepository = itemRepository;
        _produtoRepository = produtoRepository;
    }

    public async Task<ItemNotaFiscal> CreateAsync(ItemNotaFiscal item)
    {
        if (item.QuantidadeProduto <= 0)
            throw new ArgumentException("A quantidade do produto deve ser maior que zero.");

        var produto = await _produtoRepository.GetByIdAsync(item.ProdutoId);

        if (produto is null)
            throw new ArgumentException($"Não existe produto com o ID '{item.ProdutoId}'.");

        if (produto.Saldo < item.QuantidadeProduto)
            throw new InvalidOperationException(
                $"Saldo insuficiente para o produto '{produto.Descricao}'. Saldo atual: {produto.Saldo}, solicitado: {item.QuantidadeProduto}.");

        produto.Saldo -= item.QuantidadeProduto;

        await _produtoRepository.UpdateAsync(produto);

        return await _itemRepository.AddAsync(item);
    }
}