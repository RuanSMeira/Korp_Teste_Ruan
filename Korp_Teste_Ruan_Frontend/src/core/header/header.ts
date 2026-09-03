import { Component, Input, inject } from '@angular/core';
import { SessaoService } from '../../app/core/api/sessao.service';

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.html',
})
export class HeaderComponent {
  private readonly sessao = inject(SessaoService);
  @Input({ required: true }) title!: string;
  @Input({ required: true }) subtitle!: string;

  get userName(): string {
    return this.sessao.obterNomeExibicao();
  }

  get avatarUrl(): string {
    return '';
  }
}
