import { Injectable } from '@angular/core';
import { Sessao, Usuario } from './models';

@Injectable({ providedIn: 'root' })
export class SessaoService {
  private readonly usuarioKey = 'korp.usuario';
  private readonly empresaPendenteKey = 'korp.empresa-pendente';

  salvar(sessao: Sessao): void {
    localStorage.setItem(this.usuarioKey, JSON.stringify(sessao));
  }

  salvarUsuario(usuario: Usuario): void {
    this.salvar(usuario);
  }

  obterSessao(): Sessao | null {
    try {
      const valor = localStorage.getItem(this.usuarioKey);
      return valor ? JSON.parse(valor) as Sessao : null;
    } catch {
      localStorage.removeItem(this.usuarioKey);
      return null;
    }
  }

  encerrar(): void {
    localStorage.removeItem(this.usuarioKey);
    localStorage.removeItem(this.empresaPendenteKey);
  }

  obterEmpresaId(): number | null {
    const sessao = this.obterSessao();
    if (!sessao) return null;
    const valor = 'empresaId' in sessao
      ? sessao.empresaId
      : (sessao as Sessao & { EmpresaId?: number }).EmpresaId;
    const empresaId = Number(valor);
    return Number.isInteger(empresaId) && empresaId > 0 ? empresaId : null;
  }

  obterUsuarioId(): number | null {
    const sessao = this.obterSessao();
    if (!sessao) return null;
    const valor = 'usuarioId' in sessao
      ? sessao.usuarioId
      : (sessao as Sessao & { UsuarioId?: number }).UsuarioId;
    const usuarioId = Number(valor);
    return Number.isInteger(usuarioId) && usuarioId > 0 ? usuarioId : null;
  }

  obterNomeExibicao(): string {
    const sessao = this.obterSessao();
    if (!sessao) return '';
    return 'nomeUsuario' in sessao
      ? sessao.nomeUsuario
      : ('NomeUsuario' in sessao ? (sessao as Sessao & { NomeUsuario: string }).NomeUsuario : sessao.nomeFantasia);
  }

  obterEmpresaNome(): string {
    const sessao = this.obterSessao();
    if (!sessao) return '';
    const nome = 'nomeFantasia' in sessao
      ? sessao.nomeFantasia
      : (sessao as Sessao & { NomeFantasia?: string }).NomeFantasia;
    return nome || `Empresa ${this.obterEmpresaId() ?? ''}`;
  }

  obterEmpresaCnpj(): string {
    const sessao = this.obterSessao();
    const cnpj = sessao && 'cnpj' in sessao
      ? sessao.cnpj
      : (sessao as (Sessao & { Cnpj?: string }) | null)?.Cnpj;
    return cnpj || 'Não informado';
  }

  salvarEmpresaPendente(empresaId: number): void {
    localStorage.setItem(this.empresaPendenteKey, String(empresaId));
  }

  obterEmpresaPendente(): number | null {
    const empresaId = Number(localStorage.getItem(this.empresaPendenteKey));
    return Number.isInteger(empresaId) && empresaId > 0 ? empresaId : null;
  }

  limparEmpresaPendente(): void {
    localStorage.removeItem(this.empresaPendenteKey);
  }
}
