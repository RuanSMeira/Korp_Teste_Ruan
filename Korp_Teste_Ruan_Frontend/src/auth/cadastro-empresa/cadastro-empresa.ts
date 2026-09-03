import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-cadastro-empresa',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './cadastro-empresa.html'
})
export class CadastroEmpresaComponent {
  private fb = inject(FormBuilder);

  empresaForm: FormGroup = this.fb.group({
    razaoSocial: ['', Validators.required],
    cnpj: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    telefone: ['', Validators.required],
    setor: ['', Validators.required]
  });

  onSubmit() {
    if (this.empresaForm.invalid) {
      this.empresaForm.markAllAsTouched();
      return;
    }
    console.log('Payload Cadastro Empresa pronto:', this.empresaForm.value);
  }
}