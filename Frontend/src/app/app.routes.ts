import { Routes } from '@angular/router';
import { HomeComponent } from './Features/home/home';
import { UserFormComponent } from './Features/Users/user-form/user-form';
import { LoginComponent } from './Features/auth/login/login';
import { RegisterComponent } from './Features/auth/register/register';
import { authGuard } from './Core/guards/auth-guard';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'home', component: HomeComponent }, 

  // Authentication
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },

  // Protected User Routes
  { 
    path: 'users/add', 
    component: UserFormComponent, 
    canActivate: [authGuard] 
  },
  { 
    path: 'users/edit/:id', 
    component: UserFormComponent, 
    canActivate: [authGuard] 
  },

  // Protected Vehicle Routes (Lazy Loaded)
  {
    path: 'vehicles',
    canActivate: [authGuard], // Guard applied here
    loadComponent: () =>
      import('./Features/Vehicles/vehicle-list/vehicle-list')
        .then(m => m.VehicleListComponent)
  },
  {
    path: 'vehicles/add',
    canActivate: [authGuard], // Guard applied here
    loadComponent: () =>
      import('./Features/Vehicles/vehicle-form/vehicle-form')
        .then(m => m.VehicleFormComponent)
  },
  {
    path: 'vehicles/edit/:id',
    canActivate: [authGuard], // Guard applied here
    loadComponent: () =>
      import('./Features/Vehicles/vehicle-form/vehicle-form')
        .then(m => m.VehicleFormComponent)
  },

  // Fallback for undefined routes
  { path: '**', redirectTo: 'home' }
];