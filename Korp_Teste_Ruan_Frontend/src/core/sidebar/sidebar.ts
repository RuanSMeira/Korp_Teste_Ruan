import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { SessaoService } from '../../app/core/api/sessao.service';

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
  private readonly sessaoService = inject(SessaoService);
  private readonly router = inject(Router);
  readonly sessao = this.sessaoService.obterSessao();

  navItems: NavItem[] = [
    { label: 'Painel da Empresa', path: '/app/painel-empresa', icon: 'wallet' },
    { label: 'Cadastrar Produto', path: '/app/cadastrar-produto', icon: 'box' },
    { label: 'Conferir Saldo', path: '/app/saldo', icon: 'wallet' },
    { label: 'Serviço de Estoque', path: '/app/estoque', icon: 'database' },
    { label: 'Serviço de Faturamento', path: '/app/faturamento', icon: 'file-text' },
  ];

  sair(): void {
    this.sessaoService.encerrar();
    this.router.navigate(['/auth/empresa']);
  }
}
