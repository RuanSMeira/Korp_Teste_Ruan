import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

interface NavItem {
  label: string;
  path: string;
  icon: 'box' | 'wallet' | 'database' | 'file-text';
}

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './sidebar.html',
})
export class SidebarComponent {
  navItems: NavItem[] = [
    { label: 'Cadastrar Produto', path: '/cadastrar-produto', icon: 'box' },
    { label: 'Conferir Saldo', path: '/conferir-saldo', icon: 'wallet' },
    { label: 'Serviço de Estoque', path: '/servico-estoque', icon: 'database' },
    { label: 'Serviço de Faturamento', path: '/servico-faturamento', icon: 'file-text' },
  ];
}
