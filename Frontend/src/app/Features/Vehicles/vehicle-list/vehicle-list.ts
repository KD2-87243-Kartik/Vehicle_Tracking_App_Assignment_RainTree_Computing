import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { VehicleService } from '../../../Core/Services/vehicle.service';

@Component({
  selector: 'app-vehicle-list',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './vehicle-list.html',
  styleUrls: ['./vehicle-list.css']
})
export class VehicleListComponent implements OnInit {

  vehicles: any[] = [];

  searchTerm: string = '';
  pageNumber: number = 1;
  pageSize: number = 5;

  totalRecords: number = 0;
  totalPages: number = 0;

  constructor(private vehicleService: VehicleService) {}

  ngOnInit(): void {
    this.loadVehicles();
  }

  loadVehicles() {
    this.vehicleService
      .searchVehicles(this.searchTerm, this.pageNumber, this.pageSize)
      .subscribe({
        next: (res) => {
          this.vehicles = res.data;
          this.totalRecords = res.totalRecords;
          this.totalPages = Math.ceil(this.totalRecords / this.pageSize);
        },
        error: (err) => {
          console.error(err);
          alert('Failed to load vehicles');
        }
      });
  }

  search() {
    this.pageNumber = 1;
    this.loadVehicles();
  }

  nextPage() {
    if (this.pageNumber < this.totalPages) {
      this.pageNumber++;
      this.loadVehicles();
    }
  }

  previousPage() {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.loadVehicles();
    }
  }
}