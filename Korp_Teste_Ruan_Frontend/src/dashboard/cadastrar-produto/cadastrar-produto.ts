import { Component, inject } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { HeaderComponent } from '../../core/header/header';
import { ProdutoService } from '../../app/core/api/produto.service';
import { SessaoService } from '../../app/core/api/sessao.service';

@Component({
  selector: 'app-cadastrar-produto',
  standalone: true,
  imports: [HeaderComponent, ReactiveFormsModule],
  templateUrl: './cadastrar-produto.html',
})
export class CadastrarProdutoComponent {
  private fb = new FormBuilder();
  private readonly produtoService = inject(ProdutoService);
  private readonly sessao = inject(SessaoService);
  mensagem = '';
  erro = '';
  salvando = false;

  form = this.fb.group({
    codigo: ['', [Validators.required, Validators.maxLength(50)]],
    descricao: ['', [Validators.required, Validators.maxLength(500)]],
    saldoInicial: [0, [Validators.required, Validators.min(0)]],
  });

  onCancelar(): void {
    this.form.reset({ saldoInicial: 0 });
  }

  onSalvar(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.erro = 'Revise os campos obrigatórios e os valores informados.';
      return;
    }
    this.salvando = true;
    this.mensagem = '';
    this.erro = '';
    const value = this.form.getRawValue();
    const empresaId = this.sessao.obterEmpresaId();
    if (!empresaId) {
      this.erro = 'Sua sessão expirou. Faça login novamente para cadastrar produtos.';
      this.salvando = false;
      return;
    }
    this.produtoService.criar({
      empresaId,
      codigo: value.codigo ?? '',
      descricao: value.descricao ?? '',
      saldoInicial: Number(value.saldoInicial ?? 0)
    }).subscribe({
      next: (produto) => {
        this.mensagem = `Produto ${produto.codigo} cadastrado com sucesso.`;
        this.salvando = false;
        this.form.reset({ saldoInicial: 0 });
      },
      error: (error: Error) => {
        this.erro = error.message;
        this.salvando = false;
      }
    });
  }
}
