import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ApiService } from '../../app/core/api/api.service';
import { SessaoEmpresa } from '../../app/core/api/models';
import { SessaoService } from '../../app/core/api/sessao.service';

@Component({
  selector: 'app-login-empresa',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login-empresa.html'
})
export class LoginEmpresaComponent {
  private fb = inject(FormBuilder);
  private api = inject(ApiService);
  private router = inject(Router);
  private sessao = inject(SessaoService);
  erro = '';
  entrando = false;

  loginEmpresaForm: FormGroup = this.fb.group({
    cnpj: ['', [Validators.required, Validators.minLength(14)]],
    senha: ['', [Validators.required]]
  });

  onSubmit() {
    if (this.loginEmpresaForm.invalid) {
      this.loginEmpresaForm.markAllAsTouched();
      this.erro = 'Informe o CNPJ e a senha master.';
      return;
    }
    this.entrando = true;
    this.erro = '';
    const value = this.loginEmpresaForm.getRawValue();
    this.api.post<SessaoEmpresa>('Empresa/login', { cnpj: (value.cnpj ?? '').replace(/\D/g, ''), senha: value.senha }).subscribe({
      next: (sessao) => {
        this.sessao.salvar(sessao);
        this.router.navigate(['/app/painel-empresa']);
      },
      error: (error: Error) => { this.erro = error.message; this.entrando = false; }
    });
  }
}
