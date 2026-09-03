import { HttpErrorResponse, HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ApiService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = 'http://localhost:5206/api';

  get<T>(path: string): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}/${path}`).pipe(catchError(this.handleError));
  }

  post<T>(path: string, body: unknown): Observable<T> {
    return this.http.post<T>(`${this.baseUrl}/${path}`, body).pipe(catchError(this.handleError));
  }

  delete<T>(path: string): Observable<T> {
    return this.http.delete<T>(`${this.baseUrl}/${path}`).pipe(catchError(this.handleError));
  }

  put<T>(path: string, body?: unknown): Observable<T> {
    return this.http.put<T>(`${this.baseUrl}/${path}`, body).pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    const body = error.error;
    const validationMessages = body?.errors
      ? Object.values(body.errors).flat().join(' ')
      : null;
    const message = body?.erro
      ?? body?.mensagem
      ?? validationMessages
      ?? (typeof body === 'string' ? body : null)
      ?? (error.status === 0 ? 'Não foi possível conectar ao backend.' : null);
    return throwError(() => new Error(message || `Não foi possível concluir a operação (${error.status}).`));
  }
}
