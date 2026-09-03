import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { CriarNotaFiscalRequest, NotaFiscal } from './models';

@Injectable({ providedIn: 'root' })
export class NotaFiscalService {
  private readonly api = inject(ApiService);

  listarPorEmpresa(empresaId: number): Observable<NotaFiscal[]> {
    return this.api.get<NotaFiscal[]>(`NotaFiscal/empresa/${empresaId}`);
  }

  criar(request: CriarNotaFiscalRequest): Observable<NotaFiscal> {
    return this.api.post<NotaFiscal>('NotaFiscal', request);
  }

  obter(id: number): Observable<NotaFiscal> {
    return this.api.get<NotaFiscal>(`NotaFiscal/${id}`);
  }

  emitir(id: number): Observable<{ mensagem: string; nota: NotaFiscal }> {
    return this.api.put<{ mensagem: string; nota: NotaFiscal }>(`NotaFiscal/${id}/emitir`);
  }
}
