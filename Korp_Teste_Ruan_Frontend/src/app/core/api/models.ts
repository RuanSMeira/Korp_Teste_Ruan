export interface Produto {
  produtoId: number;
  empresaId: number;
  codigo: string;
  descricao: string;
  saldo: number;
  rowVersion?: string;
}

export interface CriarProdutoRequest {
  empresaId: number;
  codigo: string;
  descricao: string;
  saldoInicial: number;
}

export interface ItemNotaFiscalRequest {
  produtoId: number;
  quantidade: number;
}

export interface ItemNotaFiscalResponse extends ItemNotaFiscalRequest {
  id: number;
}

export interface NotaFiscal {
  id: number;
  empresaId: number;
  usuarioEmissorId: number;
  numeroSequencial: number;
  status: string;
  itens: ItemNotaFiscalResponse[];
}

export interface CriarNotaFiscalRequest {
  empresaId: number;
  usuarioEmissorId: number;
  itens: ItemNotaFiscalRequest[];
}

export interface Empresa {
  empresaId: number;
  razaoSocial: string;
  nomeFantasia: string;
  cnpj: string;
}

export interface CriarEmpresaRequest {
  razaoSocial: string;
  nomeFantasia: string;
  cnpj: string;
  senhaMaster: string;
}

export interface CriarUsuarioRequest {
  nomeUsuario: string;
  email: string;
  senha: string;
  empresaId: number;
}

export interface LoginRequest {
  email: string;
  senha: string;
}

export interface Usuario {
  usuarioId: number;
  empresaId: number;
  nomeUsuario: string;
  email: string;
  nomeFantasia?: string;
  cnpj?: string;
  perfil?: 'usuario';
}

export interface SessaoEmpresa {
  empresaId: number;
  usuarioId?: number;
  nomeFantasia: string;
  cnpj: string;
  perfil: 'empresa';
}

export type Sessao = Usuario | SessaoEmpresa;

export type TipoMovimentacaoEstoque = 'Entrada' | 'Saida';

export interface CriarMovimentacaoEstoqueRequest {
  empresaId: number;
  produtoId: number;
  usuarioId: number;
  tipo: TipoMovimentacaoEstoque;
  quantidade: number;
  observacao: string;
}

export interface MovimentacaoEstoque {
  id: number;
  empresaId: number;
  produtoId: number;
  produto: string;
  codigoProduto: string;
  usuarioId: number;
  responsavel: string;
  tipo: TipoMovimentacaoEstoque;
  quantidade: number;
  saldoAnterior: number;
  saldoPosterior: number;
  dataMovimentacao: string;
  observacao: string;
}
