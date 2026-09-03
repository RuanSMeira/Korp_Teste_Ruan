import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-login-empresa',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login-empresa.html'
})
export class LoginEmpresaComponent {
  private fb = inject(FormBuilder);

  loginEmpresaForm: FormGroup = this.fb.group({
    cnpj: ['', [Validators.required, Validators.minLength(14)]],
    senha: ['', [Validators.required]]
  });

  onSubmit() {
    if (this.loginEmpresaForm.invalid) {
      this.loginEmpresaForm.markAllAsTouched();
      return;
    }
    console.log('Payload Login Empresa pronto:', this.loginEmpresaForm.value);
  }
}