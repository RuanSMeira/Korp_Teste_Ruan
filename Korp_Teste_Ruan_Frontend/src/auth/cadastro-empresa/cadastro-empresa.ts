import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from '../../app/core/api/api.service';
import { SessaoService } from '../../app/core/api/sessao.service';
import { Empresa } from '../../app/core/api/models';

@Component({
  selector: 'app-cadastro-empresa',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './cadastro-empresa.html'
})
export class CadastroEmpresaComponent {
  private fb = inject(FormBuilder);
  private api = inject(ApiService);
  private router = inject(Router);
  private sessao = inject(SessaoService);
  erro = '';
  salvando = false;

  empresaForm: FormGroup = this.fb.group({
    razaoSocial: ['', Validators.required],
    nomeFantasia: ['', Validators.required],
    cnpj: ['', [Validators.required, Validators.pattern(/^[\d./-]+$/), Validators.minLength(14)]],
    senhaMaster: ['', [Validators.required, Validators.minLength(8)]]
  });

  onSubmit() {
    if (this.empresaForm.invalid) {
      this.empresaForm.markAllAsTouched();
      this.erro = 'Revise os campos destacados antes de continuar.';
      return;
    }
    this.erro = '';
    this.salvando = true;
    const value = this.empresaForm.getRawValue();
    const cnpj = (value.cnpj ?? '').replace(/\D/g, '');
    this.api.post<Empresa>('Empresa', { ...value, cnpj }).subscribe({
      next: (empresa) => {
        this.sessao.salvarEmpresaPendente(empresa.empresaId);
        this.router.navigate(['/auth/cadastro-usuario']);
      },
      error: (error: Error) => { this.erro = error.message; this.salvando = false; }
    });
  }
}
