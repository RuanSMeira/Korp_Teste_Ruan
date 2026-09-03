import { Component, computed, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { HeaderComponent } from '../../core/header/header';
import { Produto, MovimentacaoEstoque } from '../../app/core/api/models';
import { ProdutoService } from '../../app/core/api/produto.service';
import { MovimentacaoEstoqueService } from '../../app/core/api/movimentacao-estoque.service';
import { SessaoService } from '../../app/core/api/sessao.service';

@Component({
  selector: 'app-servico-estoque',
  standalone: true,
  imports: [HeaderComponent, ReactiveFormsModule],
  templateUrl: './servico-estoque.html',
})
export class ServicoEstoqueComponent {
  private readonly fb = new FormBuilder();
  private readonly produtoService = inject(ProdutoService);
  private readonly movimentacaoService = inject(MovimentacaoEstoqueService);
  private readonly sessao = inject(SessaoService);
  readonly empresaId = this.sessao.obterEmpresaId() ?? 0;
  readonly usuarioId = this.sessao.obterUsuarioId() ?? 0;
  filtro = signal<'todas' | 'entradas' | 'saidas'>('todas');
  movimentos = signal<MovimentacaoEstoque[]>([]);
  produtos = signal<Produto[]>([]);
  carregando = signal(true);
  salvando = signal(false);
  erro = signal('');
  mensagem = signal('');
  form = this.fb.group({ produtoId: [0, Validators.min(1)], tipo: ['Entrada', Validators.required], quantidade: [1, [Validators.required, Validators.min(0.0001)]], observacao: [''] });

  movimentosFiltrados = computed(() => {
    const f = this.filtro();
    if (f === 'todas') return this.movimentos();
    const tipo = f === 'entradas' ? 'Entrada' : 'Saida';
    return this.movimentos().filter((m) => m.tipo === tipo);
  });

  get movimentosHoje(): MovimentacaoEstoque[] {
    const hoje = new Date();
    return this.movimentos().filter((movimento) => {
      const data = new Date(movimento.dataMovimentacao);
      return data.getFullYear() === hoje.getFullYear()
        && data.getMonth() === hoje.getMonth()
        && data.getDate() === hoje.getDate();
    });
  }

  constructor() {
    if (!this.empresaId) {
      this.erro.set('Não foi possível identificar a empresa da sessão.');
      this.carregando.set(false);
      return;
    }
    this.carregar();
    this.produtoService.listarPorEmpresa(this.empresaId).subscribe({ next: (produtos) => this.produtos.set(produtos) });
  }

  setFiltro(f: 'todas' | 'entradas' | 'saidas'): void {
    this.filtro.set(f);
  }

  carregar(): void {
    this.carregando.set(true);
    this.movimentacaoService.listarPorEmpresa(this.empresaId).subscribe({
      next: (movimentos) => { this.movimentos.set(movimentos); this.carregando.set(false); },
      error: (error: Error) => { this.erro.set(error.message); this.carregando.set(false); }
    });
  }

  criar(): void {
    if (!this.usuarioId) {
      this.erro.set('É necessário estar logado como usuário para registrar uma movimentação.');
      return;
    }
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.erro.set('Selecione um produto e informe uma quantidade maior que zero.');
      return;
    }
    const value = this.form.getRawValue();
    this.salvando.set(true);
    this.erro.set('');
    this.movimentacaoService.criar({ empresaId: this.empresaId, produtoId: Number(value.produtoId), usuarioId: this.usuarioId, tipo: value.tipo as 'Entrada' | 'Saida', quantidade: Number(value.quantidade), observacao: value.observacao ?? '' }).subscribe({
      next: (movimento) => { this.mensagem.set(`Movimentação registrada. Novo saldo: ${movimento.saldoPosterior}.`); this.salvando.set(false); this.form.reset({ produtoId: 0, tipo: 'Entrada', quantidade: 1, observacao: '' }); this.carregar(); this.produtoService.listarPorEmpresa(this.empresaId).subscribe({ next: (produtos) => this.produtos.set(produtos) }); },
      error: (error: Error) => { this.erro.set(error.message); this.salvando.set(false); }
    });
  }

  formatarData(data: string): string {
    return new Date(data).toLocaleString('pt-BR');
  }
}
