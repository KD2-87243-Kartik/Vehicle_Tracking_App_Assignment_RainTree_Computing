import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  private apiUrl = 'https://localhost:7010/api/Vehicles';

  constructor(private http: HttpClient) {}

  // Search + Pagination
  searchVehicles(searchTerm: string, pageNumber: number, pageSize: number): Observable<any> {
    return this.http.get(
      `${this.apiUrl}/search?searchTerm=${searchTerm}&pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
  }

  getVehicleById(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  addVehicle(vehicle: any): Observable<any> {
    return this.http.post(this.apiUrl, vehicle);
  }

  updateVehicle(id: number, vehicle: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, vehicle);
  }
}