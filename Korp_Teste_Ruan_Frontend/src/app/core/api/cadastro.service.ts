import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { CriarEmpresaRequest, CriarUsuarioRequest, Empresa, Usuario } from './models';

@Injectable({ providedIn: 'root' })
export class CadastroService {
  private readonly api = inject(ApiService);

  criarEmpresa(request: CriarEmpresaRequest): Observable<Empresa> {
    return this.api.post<Empresa>('Empresa', request);
  }

  criarUsuario(request: CriarUsuarioRequest): Observable<Usuario> {
    return this.api.post<Usuario>('Usuario', request);
  }
}
