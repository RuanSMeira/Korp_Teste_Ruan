using Korp_Teste_Ruan_Backend.Data;
using Korp_Teste_Ruan_Backend.DTOs.Request;
using Korp_Teste_Ruan_Backend.DTOs.Response;
using Korp_Teste_Ruan_Backend.Models;
using Korp_Teste_Ruan_Backend.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Korp_Teste_Ruan_Backend.Services;

public class MovimentacaoEstoqueService
{
    private readonly AppDbContext _context;

    public MovimentacaoEstoqueService(AppDbContext context) => _context = context;

    public async Task<IEnumerable<MovimentacaoEstoqueResponse>> ListarPorEmpresaAsync(int empresaId)
    {
        return await _context.MovimentacoesEstoque.AsNoTracking()
            .Include(m => m.Produto).Include(m => m.Usuario)
            .Where(m => m.EmpresaId == empresaId)
            .OrderByDescending(m => m.DataMovimentacao)
            .Select(m => Mapear(m)).ToListAsync();
    }

    public async Task<MovimentacaoEstoqueResponse> CriarAsync(CriarMovimentacaoEstoqueRequest request)
    {
        if (!await _context.Empresas.AnyAsync(e => e.EmpresaId == request.EmpresaId))
            throw new ArgumentException("Empresa não encontrada.");

        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == request.ProdutoId && p.EmpresaId == request.EmpresaId);
        if (produto is null) throw new ArgumentException("Produto não encontrado para esta empresa.");
        if (!await _context.Usuarios.AnyAsync(u => u.UsuarioId == request.UsuarioId && u.EmpresaId == request.EmpresaId))
            throw new ArgumentException("Usuário responsável não encontrado para esta empresa.");

        var saldoAnterior = produto.Saldo;
        var saldoPosterior = request.Tipo == TipoMovimentacaoEstoque.Entrada
            ? saldoAnterior + request.Quantidade
            : saldoAnterior - request.Quantidade;
        if (saldoPosterior < 0) throw new InvalidOperationException("Estoque insuficiente para realizar esta saída.");

        await using var transaction = await _context.Database.BeginTransactionAsync();
        produto.Saldo = saldoPosterior;
        var movimento = new MovimentacaoEstoque
        {
            EmpresaId = request.EmpresaId, ProdutoId = request.ProdutoId, UsuarioId = request.UsuarioId,
            Tipo = request.Tipo, Quantidade = request.Quantidade, SaldoAnterior = saldoAnterior,
            SaldoPosterior = saldoPosterior, Observacao = request.Observacao
        };
        _context.MovimentacoesEstoque.Add(movimento);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        movimento.Produto = produto;
        movimento.Usuario = await _context.Usuarios.AsNoTracking().FirstAsync(u => u.UsuarioId == request.UsuarioId);
        return Mapear(movimento);
    }

    private static MovimentacaoEstoqueResponse Mapear(MovimentacaoEstoque m) => new()
    {
        Id = m.MovimentacaoEstoqueId, EmpresaId = m.EmpresaId, ProdutoId = m.ProdutoId,
        Produto = m.Produto.Descricao, CodigoProduto = m.Produto.Codigo, UsuarioId = m.UsuarioId,
        Responsavel = m.Usuario.NomeUsuario, Tipo = m.Tipo.ToString(), Quantidade = m.Quantidade,
        SaldoAnterior = m.SaldoAnterior, SaldoPosterior = m.SaldoPosterior,
        DataMovimentacao = m.DataMovimentacao, Observacao = m.Observacao
    };
}