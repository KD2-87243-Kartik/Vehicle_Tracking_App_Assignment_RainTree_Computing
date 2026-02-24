import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../Core/Services/auth.service'
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class LoginComponent {

  loginForm: any;
  
  constructor(
  private fb: FormBuilder,
  private authService: AuthService,
  private router: Router
) {
  this.loginForm = this.fb.group({
    emailAddress: ['', Validators.required],
    password: ['', Validators.required]
  });
}
onSubmit() {
  if (this.loginForm.invalid) return;

  this.authService.login(this.loginForm.value)
    .subscribe({
      next: (res: any) => {
        // FIX: Access the .token property (or whatever your backend calls it)
        if (res && res.token) {
          localStorage.setItem('token', res.token); 
          this.router.navigate(['/home']);
        } else {
          console.error('Token not found in response', res);
        }
      },
      error: (err) => {
        console.error('Login error', err);
        alert('Invalid credentials');
      }
    });
  }
}
