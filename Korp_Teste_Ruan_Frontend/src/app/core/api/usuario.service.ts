import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { Usuario } from './models';

@Injectable({ providedIn: 'root' })
export class UsuarioService {
  private readonly api = inject(ApiService);

  listarPorEmpresa(empresaId: number): Observable<Usuario[]> {
    return this.api.get<Usuario[]>(`Usuario/empresa/${empresaId}`);
  }
}
