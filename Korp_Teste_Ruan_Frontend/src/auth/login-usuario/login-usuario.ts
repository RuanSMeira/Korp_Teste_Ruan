import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { Router } from '@angular/router';
import { ApiService } from '../../app/core/api/api.service';
import { Usuario } from '../../app/core/api/models';
import { SessaoService } from '../../app/core/api/sessao.service';

@Component({
  selector: 'app-login-usuario',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login-usuario.html'
})
export class LoginUsuarioComponent {
  private fb = inject(FormBuilder);
  private api = inject(ApiService);
  private router = inject(Router);
  private sessao = inject(SessaoService);
  erro = '';
  entrando = false;

  loginUsuarioForm: FormGroup = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    senha: ['', [Validators.required, Validators.minLength(6)]]
  });

  onSubmit() {
    if (this.loginUsuarioForm.invalid) {
      this.loginUsuarioForm.markAllAsTouched();
      this.erro = 'Informe um e-mail e senha válidos para entrar.';
      return;
    }
    this.erro = '';
    this.entrando = true;
    this.api.post<Usuario>('Usuario/login', this.loginUsuarioForm.getRawValue()).subscribe({
      next: (usuario) => {
        this.sessao.salvarUsuario(usuario);
        this.router.navigate(['/app']);
      },
      error: (error: Error) => {
        this.erro = error.message;
        this.entrando = false;
      }
    });
  }
}
