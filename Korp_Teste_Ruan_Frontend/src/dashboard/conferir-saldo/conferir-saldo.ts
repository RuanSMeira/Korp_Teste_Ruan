import { Component, inject } from '@angular/core';
import { ProdutoService } from '../../app/core/api/produto.service';
import { Produto } from '../../app/core/api/models';
import { SessaoService } from '../../app/core/api/sessao.service';


interface SaldoRow {
  sku: string;
  produto: string;
  categoria: string;
  qtd: number;
  qtdLabel: string;
  qtdColor: string;
  precoUnitario: string;
  valorTotal: string;
}

@Component({
  selector: 'app-conferir-saldo',
  standalone: true,
  imports: [],
  templateUrl: './conferir-saldo.html',
})
export class ConferirSaldoComponent {
  private readonly produtoService = inject(ProdutoService);
  private readonly sessao = inject(SessaoService);
  carregando = true;
  erro = '';
  rows: SaldoRow[] = [];
  resumo = { totalProdutos: 0, saldoTotal: 0, produtosBaixoEstoque: 0, produtosSemEstoque: 0 };

  get totalSaldo(): number {
    return this.rows.reduce((total, row) => total + row.qtd, 0);
  }

  get produtosBaixoEstoque(): number {
    return this.rows.filter((row) => row.qtd > 0 && row.qtd < 10).length;
  }

  get produtosSemEstoque(): number {
    return this.rows.filter((row) => row.qtd <= 0).length;
  }

  constructor() {
    this.carregar();
  }

  carregar(): void {
    this.carregando = true;
    this.erro = '';
    const empresaId = this.sessao.obterEmpresaId();
    if (!empresaId) {
      this.erro = 'Sua sessão expirou. Faça login novamente.';
      this.carregando = false;
      return;
    }
    this.produtoService.listarPorEmpresa(empresaId).subscribe({
      next: (produtos) => {
        const saldoTotal = produtos.reduce((total, produto) => total + produto.saldo, 0);
        this.resumo = {
          totalProdutos: produtos.length,
          saldoTotal,
          produtosBaixoEstoque: produtos.filter((produto) => produto.saldo > 0 && produto.saldo < 10).length,
          produtosSemEstoque: produtos.filter((produto) => produto.saldo <= 0).length,
        };
        this.rows = produtos.map((produto) => this.toRow(produto));
        this.carregando = false;
      },
      error: (error: Error) => {
        this.erro = error.message;
        this.carregando = false;
      }
    });
  }

  private toRow(produto: Produto): SaldoRow {
    const qtdColor = produto.saldo <= 0 ? 'text-[#EF4444]' : produto.saldo < 10 ? 'text-[#F59E0B]' : 'text-[#10B981]';
    return {
      sku: produto.codigo,
      produto: produto.descricao,
      categoria: 'Não informada',
      qtd: produto.saldo,
      qtdLabel: `${produto.saldo} un`,
      qtdColor,
      precoUnitario: 'Não informado',
      valorTotal: 'Não informado'
    };
  }

  exportarRelatorio(): void {
    const linhas = [
      ['Código', 'Produto', 'Quantidade'],
      ...this.rows.map((row) => [row.sku, row.produto, String(row.qtd)]),
    ];
    const csv = linhas.map((linha) => linha.map((valor) => `"${valor.replaceAll('"', '""')}"`).join(';')).join('\n');
    const blob = new Blob([csv], { type: 'text/csv;charset=utf-8;' });
    const link = document.createElement('a');
    link.href = URL.createObjectURL(blob);
    link.download = 'relatorio-de-saldos.csv';
    link.click();
    URL.revokeObjectURL(link.href);
  }
}
