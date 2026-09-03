import { Component } from '@angular/core';
import { NgClass } from '@angular/common';
import { HeaderComponent } from '../../core/header/header';

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
  nfe: number;
  outros: number;
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
  imports: [HeaderComponent, NgClass],
  templateUrl: './painel-empresa.html',
})
export class PainelEmpresaComponent {
  metrics: MetricCard[] = [
    {
      label: 'Produtos Cadastrados',
      value: '12.847',
      trendValue: '+12%',
      trendLabel: 'em relação ao mês anterior',
      trendPositive: true,
      iconBg: 'bg-[#E11A55]/[.08]',
      iconColor: 'text-[#E11A55]',
      icon: 'box',
    },
    {
      label: 'Saldo Atual',
      value: 'R$ 45.230,00',
      trendValue: '+8.2%',
      trendLabel: 'em relação ao mês anterior',
      trendPositive: true,
      iconBg: 'bg-[#10B981]/[.08]',
      iconColor: 'text-[#10B981]',
      icon: 'wallet',
    },
    {
      label: 'Status de Estoque',
      value: '98.4% Otimizado',
      trendValue: '+1.5%',
      trendLabel: 'em relação ao mês anterior',
      trendPositive: true,
      iconBg: 'bg-[#10B981]/[.08]',
      iconColor: 'text-[#10B981]',
      icon: 'database',
    },
    {
      label: 'NF-e Pendentes',
      value: '14 Notas',
      trendValue: '-4%',
      trendLabel: 'nos últimos 7 dias',
      trendPositive: false,
      iconBg: 'bg-[#F59E0B]/[.08]',
      iconColor: 'text-[#F59E0B]',
      icon: 'file-text',
    },
  ];

  chartData: DayBar[] = [
    { label: 'Seg', nfe: 38, outros: 51 },
    { label: 'Ter', nfe: 58, outros: 70 },
    { label: 'Qua', nfe: 109, outros: 90 },
    { label: 'Qui', nfe: 93, outros: 77 },
    { label: 'Sex', nfe: 131, outros: 115 },
    { label: 'Sáb', nfe: 48, outros: 38 },
    { label: 'Dom', nfe: 29, outros: 26 },
  ];

  activities: Activity[] = [
    {
      label: 'Parafuso Sextavado M12',
      sublabel: 'Cadastro de Produto',
      meta: 'Cód: 8934',
      time: 'Há 5 min',
      dotColor: 'bg-[#10B981]',
    },
    {
      label: 'Nota Fiscal de Saída - Filial PR',
      sublabel: 'Faturamento Emitido',
      meta: 'R$ 12.450,00',
      time: 'Há 24 min',
      dotColor: 'bg-[#10B981]',
    },
    {
      label: 'Chapa de Aço Laminado',
      sublabel: 'Atualização de Estoque',
      meta: '+250 un',
      time: 'Há 1 hora',
      dotColor: 'bg-[#10B981]',
    },
    {
      label: 'Nota Fiscal Consumidor - SP',
      sublabel: 'Faturamento Pendente',
      meta: 'R$ 1.890,00',
      time: 'Há 2 horas',
      dotColor: 'bg-[#F59E0B]',
    },
  ];
}
