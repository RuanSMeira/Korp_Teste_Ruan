import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { SessaoService } from '../../app/core/api/sessao.service';

@Component({
  selector: 'app-dashboard-layout',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './dashboard-layout.html'
})
export class DashboardLayoutComponent {
  private readonly sessao = inject(SessaoService);
  private readonly router = inject(Router);

  get nomeUsuario(): string {
    return this.sessao.obterNomeExibicao();
  }

  get nomeEmpresa(): string {
    return this.sessao.obterEmpresaNome();
  }

  get cnpjEmpresa(): string {
    return this.sessao.obterEmpresaCnpj();
  }

  encerrarSessao(): void {
    this.sessao.encerrar();
    this.router.navigate(['/auth/usuario']);
  }
}