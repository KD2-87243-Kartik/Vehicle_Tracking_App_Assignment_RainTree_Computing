import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from './Core/Services/auth.service';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class AppComponent {
  constructor(
  public authService: AuthService,
  private router: Router
) {}

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

logout() {
  this.authService.logout();
  this.router.navigate(['/login']);
}
}
