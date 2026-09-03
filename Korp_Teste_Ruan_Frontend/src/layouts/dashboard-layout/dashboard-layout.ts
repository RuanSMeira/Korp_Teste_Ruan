import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-dashboard-layout',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './dashboard-layout.html'
})
export class DashboardLayoutComponent {
  // Você pode colocar a lógica do usuário logado aqui, como pegar o nome "Ana Silva" do serviço de autenticação
}