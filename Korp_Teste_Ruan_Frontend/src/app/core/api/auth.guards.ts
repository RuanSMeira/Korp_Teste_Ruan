import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { SessaoService } from './sessao.service';

export const sessaoGuard: CanActivateFn = () => {
  const sessao = inject(SessaoService).obterSessao();
  return sessao ? true : inject(Router).createUrlTree(['/auth/empresa']);
};

export const empresaGuard: CanActivateFn = () => {
  const sessao = inject(SessaoService).obterSessao();
  if (!sessao) return inject(Router).createUrlTree(['/auth/empresa']);
  return sessao.perfil === 'empresa' ? true : inject(Router).createUrlTree(['/app/faturamento']);
};
