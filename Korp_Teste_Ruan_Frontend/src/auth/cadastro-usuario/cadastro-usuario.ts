import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { Router } from '@angular/router';
import { ApiService } from '../../app/core/api/api.service';
import { SessaoService } from '../../app/core/api/sessao.service';

@Component({
  selector: 'app-cadastro-usuario',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './cadastro-usuario.html'
})
export class CadastroUsuarioComponent {
  private fb = inject(FormBuilder);
  private api = inject(ApiService);
  private router = inject(Router);
  private sessao = inject(SessaoService);
  erro = '';
  salvando = false;

  cadastroForm: FormGroup = this.fb.group({
    nome: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    senha: ['', [Validators.required, Validators.minLength(8)]],
    confirmarSenha: ['', Validators.required]
  });

  onSubmit() {
    if (this.cadastroForm.invalid) {
      this.cadastroForm.markAllAsTouched();
      this.erro = 'Revise os campos destacados antes de continuar.';
      return;
    }
    const value = this.cadastroForm.getRawValue();
    if (value.senha !== value.confirmarSenha) {
      this.erro = 'As senhas não coincidem.';
      return;
    }
    const empresaId = this.sessao.obterEmpresaPendente();
    if (!empresaId) {
      this.erro = 'Cadastre uma empresa antes de criar o usuário.';
      return;
    }
    this.erro = '';
    this.salvando = true;
    this.api.post('Usuario', {
      empresaId,
      nomeUsuario: value.nome,
      email: value.email,
      senha: value.senha
    }).subscribe({
      next: () => {
        this.sessao.limparEmpresaPendente();
        this.router.navigate(['/auth/usuario']);
      },
      error: (error: Error) => { this.erro = error.message; this.salvando = false; }
    });
  }
}
