import { Routes } from '@angular/router';

import { Home } from './components/home/home';
import { AuthLayoutComponent } from '../layouts/auth-layout/auth-layout';
import { LoginUsuarioComponent } from '../auth/login-usuario/login-usuario';
import { LoginEmpresaComponent } from '../auth/login-empresa/login-empresa';
import { CadastroUsuarioComponent } from '../auth/cadastro-usuario/cadastro-usuario';
import { CadastroEmpresaComponent } from '../auth/cadastro-empresa/cadastro-empresa';
import { LayoutComponent } from '../core/layout/layout';
import { CadastrarProdutoComponent } from '../dashboard/cadastrar-produto/cadastrar-produto';
import { ConferirSaldoComponent } from '../dashboard/conferir-saldo/conferir-saldo';
import { ServicoFaturamentoComponent } from '../dashboard/servico-faturamento/servico-faturamento';
import { PainelEmpresaComponent } from '../dashboard/painel-empresa/painel-empresa';
import { ServicoEstoqueComponent } from '../dashboard/servico-estoque/servico-estoque';
import { empresaGuard, sessaoGuard } from './core/api/auth.guards';

export const routes: Routes = [
  { path: '', component: Home },
  {
    path: 'auth',
    component: AuthLayoutComponent,
    children: [
      { path: 'usuario', component: LoginUsuarioComponent },
      { path: 'empresa', component: LoginEmpresaComponent },
      { path: 'cadastro-usuario', component: CadastroUsuarioComponent },
      { path: 'cadastro-empresa', component: CadastroEmpresaComponent },
      { path: '', redirectTo: 'usuario', pathMatch: 'full' }
    ]
  },
  {
    path: 'app',
    component: LayoutComponent,
    canActivate: [sessaoGuard],
    children: [
      { path: '', redirectTo: 'faturamento', pathMatch: 'full' },
      { path: 'painel-empresa', component: PainelEmpresaComponent, canActivate: [empresaGuard] },
      { path: 'cadastrar-produto', component: CadastrarProdutoComponent, canActivate: [empresaGuard] },
      { path: 'saldo', component: ConferirSaldoComponent, canActivate: [empresaGuard] },
      { path: 'estoque', component: ServicoEstoqueComponent, canActivate: [empresaGuard] },
      { path: 'faturamento', component: ServicoFaturamentoComponent }
    ]
  }
];
