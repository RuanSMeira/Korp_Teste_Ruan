import { Component, signal, computed } from '@angular/core';
import { HeaderComponent } from '../../core/header/header';

type TipoMovimento = 'Entrada' | 'Saída';

interface Movimento {
  data: string;
  tipo: TipoMovimento;
  produto: string;
  quantidade: string;
  responsavel: string;
  observacao: string;
}

@Component({
  selector: 'app-servico-estoque',
  standalone: true,
  imports: [HeaderComponent],
  templateUrl: './servico-estoque.html',
})
export class ServicoEstoqueComponent {
  filtro = signal<'todas' | 'entradas' | 'saidas'>('todas');

  movimentos: Movimento[] = [
    { data: '24/10/2024', tipo: 'Entrada', produto: 'Parafuso Sextavado M12', quantidade: '+250 un', responsavel: 'Carlos Eduardo', observacao: 'Recebimento de fornecedor MetalLtda' },
    { data: '24/10/2024', tipo: 'Saída', produto: 'Chapa de Aço Laminado 2mm', quantidade: '-15 un', responsavel: 'Marcos Souza', observacao: 'Atendimento OP #9042' },
    { data: '24/10/2024', tipo: 'Saída', produto: 'Cabo de Cobre Flexível 4mm²', quantidade: '-40 un', responsavel: 'Felipe Neto', observacao: 'Ordem de Manutenção Predial' },
    { data: '23/10/2024', tipo: 'Entrada', produto: 'Válvula Hidráulica Direcional', quantidade: '+5 un', responsavel: 'Ana Paula', observacao: 'Retorno de conserto garantia' },
    { data: '23/10/2024', tipo: 'Entrada', produto: 'Chave Inglesa Ajustável', quantidade: '+2 un', responsavel: 'Carlos Eduardo', observacao: 'Reposição de ferramentas' },
  ];

  movimentosFiltrados = computed(() => {
    const f = this.filtro();
    if (f === 'todas') return this.movimentos;
    const tipo: TipoMovimento = f === 'entradas' ? 'Entrada' : 'Saída';
    return this.movimentos.filter((m) => m.tipo === tipo);
  });

  setFiltro(f: 'todas' | 'entradas' | 'saidas'): void {
    this.filtro.set(f);
  }
}
