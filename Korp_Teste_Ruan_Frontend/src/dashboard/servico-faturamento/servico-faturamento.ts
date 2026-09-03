import { Component, computed, inject, signal } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { HeaderComponent } from '../../core/header/header';
import { NotaFiscalService } from '../../app/core/api/nota-fiscal.service';
import { ProdutoService } from '../../app/core/api/produto.service';
import { NotaFiscal, Produto, Usuario } from '../../app/core/api/models';
import { SessaoService } from '../../app/core/api/sessao.service';
import { UsuarioService } from '../../app/core/api/usuario.service';

type StatusNota = 'Aberta' | 'Fechada';

@Component({
  selector: 'app-servico-faturamento',
  standalone: true,
  imports: [HeaderComponent, ReactiveFormsModule],
  templateUrl: './servico-faturamento.html',
})
export class ServicoFaturamentoComponent {
  private readonly fb = new FormBuilder();
  private readonly notaFiscalService = inject(NotaFiscalService);
  private readonly produtoService = inject(ProdutoService);
  private readonly sessao = inject(SessaoService);
  private readonly usuarioService = inject(UsuarioService);
  readonly perfil = this.sessao.obterSessao()?.perfil;
  readonly empresaId = this.sessao.obterEmpresaId() ?? 0;
  readonly usuarioEmissorId = this.sessao.obterUsuarioId() ?? 0;
  filtro = signal<'todas' | 'abertas' | 'fechadas'>('todas');
  notas = signal<NotaFiscal[]>([]);
  produtos = signal<Produto[]>([]);
  usuarios = signal<Usuario[]>([]);
  carregando = signal(true);
  salvando = signal(false);
  emitindoId = signal<number | null>(null);
  erro = signal('');
  mensagem = signal('');
  form = this.fb.group({ produtoId: [0, Validators.min(1)], quantidade: [1, [Validators.required, Validators.min(1)]], usuarioEmissorId: [0, Validators.min(1)] });

  notasFiltradas = computed(() => {
    const filtro = this.filtro();
    const status = filtro === 'abertas' ? 'aberta' : filtro === 'fechadas' ? 'fechada' : null;
    return this.notas().filter((nota) => !status || nota.status.toLowerCase() === status);
  });

  constructor() {
    if (!this.empresaId) {
      this.erro.set('Sua sessão expirou. Faça login novamente.');
      this.carregando.set(false);
      return;
    }
    this.carregar();
    this.produtoService.listarPorEmpresa(this.empresaId).subscribe({ next: (produtos) => this.produtos.set(produtos) });
    this.usuarioService.listarPorEmpresa(this.empresaId).subscribe({ next: (usuarios) => this.usuarios.set(usuarios), error: (error: Error) => this.erro.set(error.message) });
  }

  setFiltro(f: 'todas' | 'abertas' | 'fechadas'): void {
    this.filtro.set(f);
  }

  carregar(): void {
    this.carregando.set(true);
    this.notaFiscalService.listarPorEmpresa(this.empresaId).subscribe({
      next: (notas) => { this.notas.set(notas); this.carregando.set(false); },
      error: (error: Error) => { this.erro.set(error.message); this.carregando.set(false); }
    });
  }

  criarNota(): void {
    if (!this.empresaId) {
      this.erro.set('Sua sessão expirou. Faça login novamente.');
      return;
    }
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.erro.set('Selecione um produto e informe uma quantidade maior que zero.');
      return;
    }
    const value = this.form.getRawValue();
    const usuarioEmissorId = this.perfil === 'usuario' ? this.usuarioEmissorId : Number(value.usuarioEmissorId);
    if (!usuarioEmissorId) {
      this.erro.set('Selecione o usuário que será o emissor da nota fiscal.');
      return;
    }
    this.salvando.set(true);
    this.erro.set('');
    const request = {
      empresaId: this.empresaId,
      usuarioEmissorId,
      itens: [{ produtoId: Number(value.produtoId), quantidade: Number(value.quantidade) }],
    };
    this.notaFiscalService.criar(request).subscribe({
      next: () => { this.mensagem.set('Nota fiscal criada com status Aberta.'); this.salvando.set(false); this.form.reset({ produtoId: 0, quantidade: 1, usuarioEmissorId: 0 }); this.carregar(); },
      error: (error: Error) => { this.erro.set(error.message); this.salvando.set(false); }
    });
  }

  emitir(nota: NotaFiscal): void {
    if (this.perfil !== 'usuario') return;
    if (nota.status !== 'Aberta') return;
    this.emitindoId.set(nota.id);
    this.erro.set('');
    this.notaFiscalService.emitir(nota.id).subscribe({
      next: () => { this.mensagem.set(`Nota ${nota.numeroSequencial} fechada e estoque atualizado.`); this.emitindoId.set(null); this.carregar(); },
      error: (error: Error) => { this.erro.set(error.message); this.emitindoId.set(null); }
    });
  }

  totalItens(total: number, item: { quantidade: number }): number {
    return total + item.quantidade;
  }

  statusClasses(status: string): string {
    switch (status) {
      case 'Fechada':
        return 'bg-[#10B981]/[.08] text-[#10B981]';
      case 'Aberta':
        return 'bg-[#F59E0B]/[.08] text-[#F59E0B]';
      default:
        return 'bg-[#9CA3AF]/[.08] text-[#6B7280]';
    }
  }
}
