import { Component, ViewChild } from '@angular/core';
import { FormsModule, NgForm} from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent {
  @ViewChild('loginForm')
  loginForm!: NgForm;

  user = {
    name: '',
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
