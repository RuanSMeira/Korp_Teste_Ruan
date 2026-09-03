import { Component } from '@angular/core';
import { HeaderComponent } from '../../core/header/header';


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
  imports: [HeaderComponent],
  templateUrl: './conferir-saldo.html',
})
export class ConferirSaldoComponent {
  rows: SaldoRow[] = [
    { sku: 'SKU-8934-M', produto: 'Parafuso Sextavado M12 x 50mm', categoria: 'Fixadores', qtd: 2400, qtdLabel: '2.400 un', qtdColor: 'text-[#10B981]', precoUnitario: 'R$ 1,20', valorTotal: 'R$ 2.880,00' },
    { sku: 'SKU-5049-L', produto: 'Chapa de Aço Laminado a Quente 2mm', categoria: 'Metalurgia', qtd: 450, qtdLabel: '450 un', qtdColor: 'text-[#10B981]', precoUnitario: 'R$ 150,00', valorTotal: 'R$ 67.500,00' },
    { sku: 'SKU-3121-C', produto: 'Cabo de Cobre Flexível 4mm² 100m', categoria: 'Elétrica', qtd: 120, qtdLabel: '120 un', qtdColor: 'text-[#10B981]', precoUnitario: 'R$ 380,00', valorTotal: 'R$ 45.600,00' },
    { sku: 'SKU-1022-V', produto: 'Válvula Hidráulica Direcional G1/2', categoria: 'Pneumática', qtd: 28, qtdLabel: '28 un', qtdColor: 'text-[#10B981]', precoUnitario: 'R$ 950,00', valorTotal: 'R$ 26.600,00' },
    { sku: 'SKU-9941-K', produto: 'Chave Inglesa Ajustável Profissional 12"', categoria: 'Ferramentas', qtd: 12, qtdLabel: '12 un', qtdColor: 'text-[#F59E0B]', precoUnitario: 'R$ 85,00', valorTotal: 'R$ 1.020,00' },
    { sku: 'SKU-6652-S', produto: 'Eletrodo Revestido AWS E6013 3.2mm', categoria: 'Consumíveis', qtd: 0, qtdLabel: '0 un', qtdColor: 'text-[#EF4444]', precoUnitario: 'R$ 42,00', valorTotal: 'R$ 0,00' },
    { sku: 'SKU-4023-F', produto: 'Filtro de Ar Industrial F-200', categoria: 'Filtros', qtd: 4, qtdLabel: '4 un', qtdColor: 'text-[#F59E0B]', precoUnitario: 'R$ 310,00', valorTotal: 'R$ 1.240,00' },
  ];

  exportarRelatorio(): void {
    // TODO: integrar exportação real (CSV/PDF)
    console.log('Exportando relatório de saldos...');
  }
}
