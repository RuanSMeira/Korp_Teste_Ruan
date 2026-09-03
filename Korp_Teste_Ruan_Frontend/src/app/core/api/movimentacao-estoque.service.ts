import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { CriarMovimentacaoEstoqueRequest, MovimentacaoEstoque } from './models';

@Injectable({ providedIn: 'root' })
export class MovimentacaoEstoqueService {
  private readonly api = inject(ApiService);

  listarPorEmpresa(empresaId: number): Observable<MovimentacaoEstoque[]> {
    return this.api.get<MovimentacaoEstoque[]>(`movimentacoesestoque/empresa/${empresaId}`);
  }

  criar(request: CriarMovimentacaoEstoqueRequest): Observable<MovimentacaoEstoque> {
    return this.api.post<MovimentacaoEstoque>('movimentacoesestoque', request);
  }
}