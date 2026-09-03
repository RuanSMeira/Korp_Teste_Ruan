import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { CriarProdutoRequest, Produto, SaldoEstoque } from './models';

@Injectable({ providedIn: 'root' })
export class ProdutoService {
  private readonly api = inject(ApiService);

  listarPorEmpresa(empresaId: number): Observable<Produto[]> {
    return this.api.get<Produto[]>(`Produtos/empresa/${empresaId}`);
  }

  obterSaldo(empresaId: number): Observable<SaldoEstoque> {
    return this.api.get<SaldoEstoque>(`Produtos/empresa/${empresaId}/saldo`);
  }

  criar(request: CriarProdutoRequest): Observable<Produto> {
    return this.api.post<Produto>('Produtos', request);
  }
}
