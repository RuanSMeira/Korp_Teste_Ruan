import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { CriarProdutoRequest, Produto } from './models';

@Injectable({ providedIn: 'root' })
export class ProdutoService {
  private readonly api = inject(ApiService);

  listarPorEmpresa(empresaId: number): Observable<Produto[]> {
    return this.api.get<Produto[]>(`Produtos/empresa/${empresaId}`);
  }

  criar(request: CriarProdutoRequest): Observable<Produto> {
    return this.api.post<Produto>('Produtos', request);
  }
}
