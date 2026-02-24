import { inject } from '@angular/core';
import { Router, CanActivateFn } from '@angular/router';
import { AuthService } from '../Services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService.isLoggedIn()) {
    return true; // Access granted
  } else {
    // Access denied: Redirect to login and save the attempted URL
    router.navigate(['/login'], { queryParams: { returnUrl: state.url }});
    return false;
  }
};