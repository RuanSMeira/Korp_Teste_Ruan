import { Component } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { HeaderComponent } from '../../core/header/header';

@Component({
  selector: 'app-cadastrar-produto',
  standalone: true,
  imports: [HeaderComponent, ReactiveFormsModule],
  templateUrl: './cadastrar-produto.html',
})
export class CadastrarProdutoComponent {
  private fb = new FormBuilder();

  categorias = ['Fixadores', 'Metalurgia', 'Elétrica', 'Pneumática', 'Ferramentas', 'Consumíveis', 'Filtros'];
  unidades = ['Unidade (un)', 'Metro (m)', 'Kilograma (kg)', 'Litro (l)', 'Caixa (cx)'];

  form = this.fb.group({
    nome: ['', Validators.required],
    sku: ['', Validators.required],
    categoria: [''],
    unidade: [''],
    precoCusto: [0, [Validators.required, Validators.min(0)]],
    precoVenda: [0, [Validators.required, Validators.min(0)]],
    estoqueInicial: [0, [Validators.required, Validators.min(0)]],
    descricao: [''],
  });

  onCancelar(): void {
    this.form.reset({ precoCusto: 0, precoVenda: 0, estoqueInicial: 0 });
  }

  onSalvar(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    // TODO: integrar com o serviço de produtos
    console.log('Produto a salvar:', this.form.value);
  }
}
