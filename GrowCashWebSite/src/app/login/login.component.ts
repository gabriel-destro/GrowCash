import { Component, ViewChild } from '@angular/core';
import { FormsModule, NgForm} from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  @ViewChild('loginForm')
  loginForm!: NgForm;

  user = {
    email: '',
    password: ''
  };


  onSubmit() {
    if (this.loginForm.valid) {
      // Lógica para processar o login aqui
      //console.log('Login realizado:', this.user);
    } else {
      alert("Preencha os campos")
    }
  }
}
