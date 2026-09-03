import { Component, OnInit, inject } from '@angular/core';
import { NgClass } from '@angular/common';
import { RouterLink } from '@angular/router';
import { forkJoin } from 'rxjs';
import { HeaderComponent } from '../../core/header/header';
import { SessaoService } from '../../app/core/api/sessao.service';
import { ProdutoService } from '../../app/core/api/produto.service';
import { NotaFiscalService } from '../../app/core/api/nota-fiscal.service';
import { MovimentacaoEstoqueService } from '../../app/core/api/movimentacao-estoque.service';
import { MovimentacaoEstoque } from '../../app/core/api/models';

interface MetricCard {
  label: string;
  value: string;
  trendValue: string;
  trendLabel: string;
  trendPositive: boolean;
  iconBg: string;
  iconColor: string;
  icon: 'box' | 'wallet' | 'database' | 'file-text';
}

interface DayBar {
  label: string;
  entradas: number;
  saidas: number;
}

interface Activity {
  label: string;
  sublabel: string;
  meta: string;
  time: string;
  dotColor: string;
}

@Component({
  selector: 'app-painel-empresa',
  standalone: true,
  imports: [HeaderComponent, NgClass, RouterLink],
  templateUrl: './painel-empresa.html',
})
export class PainelEmpresaComponent implements OnInit {
  private readonly sessao = inject(SessaoService);
  private readonly produtosService = inject(ProdutoService);
  private readonly notasService = inject(NotaFiscalService);
  private readonly movimentacoesService = inject(MovimentacaoEstoqueService);
  metrics: MetricCard[] = [];
  chartData: DayBar[] = [];
  activities: Activity[] = [];
  carregando = true;
  erro = '';

  ngOnInit(): void {
    const empresaId = this.sessao.obterEmpresaId();
    if (!empresaId) {
      this.carregando = false;
      this.erro = 'Não foi possível identificar a empresa da sessão.';
      return;
    }

    forkJoin({
      produtos: this.produtosService.listarPorEmpresa(empresaId),
      notas: this.notasService.listarPorEmpresa(empresaId),
      movimentacoes: this.movimentacoesService.listarPorEmpresa(empresaId),
    }).subscribe({
      next: ({ produtos, notas, movimentacoes }) => {
        const saldoTotal = produtos.reduce((total, produto) => total + produto.saldo, 0);
        const produtosComSaldo = produtos.filter((produto) => produto.saldo > 0).length;
        const estoqueOtimizado = produtos.length ? Math.round((produtosComSaldo / produtos.length) * 100) : 0;
        const notasPendentes = notas.filter((nota) => nota.status.toLowerCase() !== 'emitida').length;
        this.metrics = [
          this.metric('Produtos Cadastrados', produtos.length.toLocaleString('pt-BR'), 'box', 'bg-[#E11A55]/[.08]', 'text-[#E11A55]'),
          this.metric('Saldo Atual', `${saldoTotal.toLocaleString('pt-BR')} un`, 'wallet', 'bg-[#10B981]/[.08]', 'text-[#10B981]'),
          this.metric('Status de Estoque', `${estoqueOtimizado}% com saldo`, 'database', 'bg-[#10B981]/[.08]', 'text-[#10B981]'),
          this.metric('NF-e Pendentes', `${notasPendentes} notas`, 'file-text', 'bg-[#F59E0B]/[.08]', 'text-[#F59E0B]', notasPendentes === 0),
        ];
        this.chartData = this.criarGrafico(movimentacoes);
        this.activities = movimentacoes.slice(0, 5).map((movimentacao) => ({
          label: movimentacao.produto || `Produto ${movimentacao.produtoId}`,
          sublabel: `${movimentacao.tipo} de estoque`,
          meta: `${movimentacao.quantidade.toLocaleString('pt-BR')} un`,
          time: new Date(movimentacao.dataMovimentacao).toLocaleDateString('pt-BR'),
          dotColor: movimentacao.tipo.toLowerCase() === 'entrada' ? 'bg-[#10B981]' : 'bg-[#E11A55]',
        }));
        this.carregando = false;
      },
      error: (error: Error) => { this.erro = error.message; this.carregando = false; },
    });
  }

  private metric(label: string, value: string, icon: MetricCard['icon'], iconBg: string, iconColor: string, positive = true): MetricCard {
    return { label, value, trendValue: positive ? 'Atual' : 'Atenção', trendLabel: 'dados da empresa', trendPositive: positive, iconBg, iconColor, icon };
  }

  private criarGrafico(movimentacoes: MovimentacaoEstoque[]): DayBar[] {
    const dias = ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb'];
    return dias.map((label, dia) => ({
      label,
      entradas: movimentacoes.filter((item) => new Date(item.dataMovimentacao).getDay() === dia && item.tipo.toLowerCase() === 'entrada').length,
      saidas: movimentacoes.filter((item) => new Date(item.dataMovimentacao).getDay() === dia && item.tipo.toLowerCase() === 'saida').length,
    }));
  }
}
