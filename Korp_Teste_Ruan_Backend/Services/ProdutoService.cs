namespace Korp_Teste_Ruan_Backend.Services;

using Microsoft.EntityFrameworkCore;
using Korp_Teste_Ruan_Backend.Models;
using Korp_Teste_Ruan_Backend.Repositories;
using Korp_Teste_Ruan_Backend.Data;

public class ProdutoService
{
    private readonly IProdutoRepository _repository;

    public ProdutoService(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Produto>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IEnumerable<Produto>> GetByEmpresaIdAsync(int empresaId)
    {
        return await _repository.GetByEmpresaIdAsync(empresaId);
    }

    public async Task<Produto?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }


    public async Task<Produto> CreateAsync(Produto produto)
    {
        Validar(produto);
        var existeOutroComMesmoCodigo = await _repository.GetByCodigoAsync(produto.EmpresaId, produto.Codigo);
        if (existeOutroComMesmoCodigo is not null)
            throw new InvalidOperationException($"Já existe outro produto com o código '{produto.Codigo}' para essa empresa.");
        return await _repository.AddAsync(produto);
    }

    public async Task<Produto?> UpdateAsync(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
            throw new ArgumentException("O ID informado não corresponde ao ID do produto.");

        Validar(produto);

        var existeOutroComMesmoCodigo = await _repository.GetByCodigoAsync(produto.EmpresaId, produto.Codigo);
        if (existeOutroComMesmoCodigo is not null && existeOutroComMesmoCodigo.ProdutoId != id)
            throw new InvalidOperationException($"Já existe outro produto com o código '{produto.Codigo}' para essa empresa.");

        try
        {
            return await _repository.UpdateAsync(produto);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new InvalidOperationException("O produto foi modificado por outro usuário. Recarregue os dados e tente novamente.");
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static void Validar(Produto produto)
    {
        if (string.IsNullOrWhiteSpace(produto.Codigo))
            throw new ArgumentException("O código do produto é obrigatório.");

        if (string.IsNullOrWhiteSpace(produto.Descricao))
            throw new ArgumentException("A descrição do produto é obrigatória.");

        if (produto.Saldo < 0)
            throw new ArgumentException("O saldo do produto não pode ser negativo.");
    }
}