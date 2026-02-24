import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7010/api/Users';

  constructor(private http: HttpClient) {}

  login(data: any) {
    return this.http.post(`${this.apiUrl}/login`, data);
  }

  register(data: any) {
    return this.http.post(`${this.apiUrl}/register`, data);
  }

  logout() {
    localStorage.removeItem('token');
  }

// auth.service.ts
getCurrentUserId(): number | null {
  const token = localStorage.getItem('token');
  if (!token || token === '[object Object]') return null; //

  try {
    const decoded: any = jwtDecode(token);
    console.log("Full Decoded Token:", decoded); 
    
    const id = decoded.nameid || decoded.sub || decoded.id || decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
    
    return id ? Number(id) : null;
  } catch (error) {
    console.error("Error decoding token:", error);
    return null;
  }
}
  
  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }
}