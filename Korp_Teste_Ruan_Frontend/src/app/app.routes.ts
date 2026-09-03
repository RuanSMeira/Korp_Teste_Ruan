import { Routes } from '@angular/router';

import { Home } from './components/home/home';
import { AuthLayoutComponent } from '../layouts/auth-layout/auth-layout';
import { LoginUsuarioComponent } from '../auth/login-usuario/login-usuario';
import { LoginEmpresaComponent } from '../auth/login-empresa/login-empresa';
import { CadastroUsuarioComponent } from '../auth/cadastro-usuario/cadastro-usuario';
import { CadastroEmpresaComponent } from '../auth/cadastro-empresa/cadastro-empresa';

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
  }
];