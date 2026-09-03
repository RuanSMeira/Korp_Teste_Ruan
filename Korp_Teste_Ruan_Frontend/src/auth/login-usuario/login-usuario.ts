import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-login-usuario',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login-usuario.html'
})
export class LoginUsuarioComponent {
  private fb = inject(FormBuilder);

  loginUsuarioForm: FormGroup = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    senha: ['', [Validators.required, Validators.minLength(6)]]
  });

  onSubmit() {
    if (this.loginUsuarioForm.invalid) {
      this.loginUsuarioForm.markAllAsTouched();
      return;
    }
    console.log('Payload Login Usuário pronto:', this.loginUsuarioForm.value);
  }
}