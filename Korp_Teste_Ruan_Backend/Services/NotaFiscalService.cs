namespace Korp_Teste_Ruan_Backend.Services;

using Korp_Teste_Ruan_Backend.DTOs.Request;
using Korp_Teste_Ruan_Backend.DTOs.Response;
using Korp_Teste_Ruan_Backend.Models;
using Korp_Teste_Ruan_Backend.Models.Enums;
using Korp_Teste_Ruan_Backend.Repositories;
using Korp_Teste_Ruan_Backend.Services.Interfaces;

public class NotaFiscalService : INotaFiscalService
{
    private readonly INotaFiscalRepository _notaFiscalRepository;
    private readonly ItemNotaFiscalService _itemNotaFiscalService; // classe concreta, sem interface
    private readonly IProdutoRepository _produtoRepository;
    private readonly IItemNotaFiscalRepository _itemNotaFiscalRepository;

    public NotaFiscalService(
        INotaFiscalRepository notaFiscalRepository,
        ItemNotaFiscalService itemNotaFiscalService,
        IProdutoRepository produtoRepository,
        IItemNotaFiscalRepository itemNotaFiscalRepository)
    {
        _notaFiscalRepository = notaFiscalRepository;
        _itemNotaFiscalService = itemNotaFiscalService;
        _produtoRepository = produtoRepository;
        _itemNotaFiscalRepository = itemNotaFiscalRepository;
    }

    public async Task<NotaFiscalResponse> CreateComItensAsync(CriarNotaFiscalRequest request)
    {
        if (request.Itens is null || request.Itens.Count == 0)
            throw new ArgumentException("A nota fiscal deve ter ao menos um item.");

        var empresaExiste = await _notaFiscalRepository.EmpresaExistsAsync(request.EmpresaId);
        if (!empresaExiste)
            throw new ArgumentException($"Não existe empresa com o ID '{request.EmpresaId}'.");

        var usuarioExiste = await _notaFiscalRepository.UsuarioExistsAsync(request.UsuarioEmissorId);
        if (!usuarioExiste)
            throw new ArgumentException($"Não existe usuário com o ID '{request.UsuarioEmissorId}'.");

        var nota = new NotaFiscal
        {
            EmpresaId = request.EmpresaId,
            UsuarioEmissorId = request.UsuarioEmissorId,
            NumeroSequencial = await _notaFiscalRepository.GetProximoNumeroSequencialAsync(request.EmpresaId)
        };

        nota = await _notaFiscalRepository.AddAsync(nota);

        var itensResponse = new List<ItemNotaFiscalResponse>();

        foreach (var itemReq in request.Itens)
        {
            var novoItem = new ItemNotaFiscal
            {
                NotaFiscalId = nota.NotaFiscalId,
                ProdutoId = itemReq.ProdutoId,
                QuantidadeProduto = itemReq.Quantidade
            };

            var criado = await _itemNotaFiscalService.CreateAsync(novoItem);

            itensResponse.Add(new ItemNotaFiscalResponse
            {
                Id = criado.ItemId,
                ProdutoId = criado.ProdutoId,
                Quantidade = criado.QuantidadeProduto
            });
        }

        return new NotaFiscalResponse
        {
            Id = nota.NotaFiscalId,
            EmpresaId = nota.EmpresaId,
            UsuarioEmissorId = nota.UsuarioEmissorId,
            NumeroSequencial = nota.NumeroSequencial,
            Status = nota.Status.ToString(),
            Itens = itensResponse
        };
    }

    public async Task<NotaFiscal?> GetByIdAsync(int id)
    {
        return await _notaFiscalRepository.GetByIdAsync(id);
    }

    public async Task<NotaFiscalResponse> EmitirNotaAsync(int notaFiscalId)
    {
        var notaFiscal = await _notaFiscalRepository.GetByIdAsync(notaFiscalId);

        if (notaFiscal is null)
            throw new ArgumentException("Nota fiscal não encontrada.");

        if (notaFiscal.Status != StatusNotaFiscal.Aberta)
            throw new InvalidOperationException("Esta nota fiscal já foi emitida e não pode ser emitida novamente.");

        var itens = (await _itemNotaFiscalRepository.GetByNotaFiscalIdAsync(notaFiscalId)).ToList();

        if (itens.Count == 0)
            throw new InvalidOperationException("Não é possível emitir uma nota fiscal sem itens.");

        // ✅ Agrupa por produto e SOMA as quantidades, caso o mesmo produto apareça em mais de um item
        var quantidadesPorProduto = itens
            .GroupBy(i => i.ProdutoId)
            .ToDictionary(g => g.Key, g => g.Sum(i => i.QuantidadeProduto));

        var produtos = new Dictionary<int, Produto>();

        foreach (var (produtoId, quantidadeTotal) in quantidadesPorProduto)
        {
            var produto = await _produtoRepository.GetByIdAsync(produtoId);

            if (produto is null)
                throw new ArgumentException($"Produto ID {produtoId} não encontrado.");

            if (produto.Saldo < quantidadeTotal)
                throw new InvalidOperationException(
                    $"Estoque insuficiente para o produto '{produto.Descricao}'. Saldo: {produto.Saldo}, necessário: {quantidadeTotal}.");

            produtos[produtoId] = produto;
        }

        // ✅ Debita e salva UMA ÚNICA VEZ por produto, evitando o conflito de RowVersion
        foreach (var (produtoId, quantidadeTotal) in quantidadesPorProduto)
        {
            var produto = produtos[produtoId];
            produto.Saldo -= quantidadeTotal;
            await _produtoRepository.UpdateAsync(produto);
        }

        notaFiscal.Status = StatusNotaFiscal.Fechada;
        notaFiscal.DataFechamento = DateTime.UtcNow;

        await _notaFiscalRepository.UpdateAsync(notaFiscal);

        return new NotaFiscalResponse
        {
            Id = notaFiscal.NotaFiscalId,
            EmpresaId = notaFiscal.EmpresaId,
            UsuarioEmissorId = notaFiscal.UsuarioEmissorId,
            NumeroSequencial = notaFiscal.NumeroSequencial,
            Status = notaFiscal.Status.ToString(),
            Itens = itens.Select(i => new ItemNotaFiscalResponse
            {
                Id = i.ItemId,
                ProdutoId = i.ProdutoId,
                Quantidade = i.QuantidadeProduto
            }).ToList()
        };
    }
}