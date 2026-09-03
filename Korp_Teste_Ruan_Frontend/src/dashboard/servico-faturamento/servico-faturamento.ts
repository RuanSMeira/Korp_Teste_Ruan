import { Component, signal, computed } from '@angular/core';
import { HeaderComponent } from '../../core/header/header';

type StatusNota = 'Emitida' | 'Pendente' | 'Cancelada';

interface Nota {
  numero: string;
  data: string;
  cliente: string;
  valor: string;
  status: StatusNota;
}

@Component({
  selector: 'app-servico-faturamento',
  standalone: true,
  imports: [HeaderComponent],
  templateUrl: './servico-faturamento.html',
})
export class ServicoFaturamentoComponent {
  filtro = signal<'todas' | 'emitidas' | 'pendentes' | 'canceladas'>('todas');

  notas: Nota[] = [
    { numero: 'NF-e 28934', data: '24/10/2024', cliente: 'Metalúrgica Paraná S/A', valor: 'R$ 12.450,00', status: 'Emitida' },
    { numero: 'NF-e 28933', data: '24/10/2024', cliente: 'Eletro Construtora Sul', valor: 'R$ 3.820,00', status: 'Emitida' },
    { numero: 'NF-e 28932', data: '23/10/2024', cliente: 'Distribuidora de Tubos Ltda', valor: 'R$ 45.200,00', status: 'Pendente' },
    { numero: 'NF-e 28931', data: '23/10/2024', cliente: 'Mecânica Auto Giro', valor: 'R$ 840,00', status: 'Emitida' },
    { numero: 'NF-e 28930', data: '22/10/2024', cliente: 'Comércio de Ferragens Iguaçu', valor: 'R$ 1.950,00', status: 'Cancelada' },
  ];

  notasFiltradas = computed(() => {
    const f = this.filtro();
    if (f === 'todas') return this.notas;
    const status: StatusNota = f === 'emitidas' ? 'Emitida' : f === 'pendentes' ? 'Pendente' : 'Cancelada';
    return this.notas.filter((n) => n.status === status);
  });

  setFiltro(f: 'todas' | 'emitidas' | 'pendentes' | 'canceladas'): void {
    this.filtro.set(f);
  }

  statusClasses(status: StatusNota): string {
    switch (status) {
      case 'Emitida':
        return 'bg-[#10B981]/[.08] text-[#10B981]';
      case 'Pendente':
        return 'bg-[#F59E0B]/[.08] text-[#F59E0B]';
      case 'Cancelada':
        return 'bg-[#EF4444]/[.08] text-[#EF4444]';
    }
  }
}
