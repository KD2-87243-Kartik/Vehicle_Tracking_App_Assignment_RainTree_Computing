import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { VehicleService } from '../../../Core/Services/vehicle.service';
import { AuthService } from '../../../Core/Services/auth.service';

@Component({
  selector: 'app-vehicle-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './vehicle-form.html',
  styleUrls: ['./vehicle-form.css']
})
export class VehicleFormComponent implements OnInit {

  vehicleForm!: FormGroup;
  isEditMode: boolean = false;
  vehicleId!: number;

  constructor(
    private fb: FormBuilder,
    private vehicleService: VehicleService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {

    this.vehicleForm = this.fb.group({
    vehicleNumber: ['', Validators.required],
    vehicleType: ['', Validators.required],
    chassisNumber: ['', Validators.required],
    engineNumber: ['', Validators.required],
    manufacturingYear: ['', Validators.required],
    loadCarryingCapacity: ['', Validators.required],
    makeOfVehicle: ['', Validators.required],
    modelNumber: ['', Validators.required],
    bodyType: ['', Validators.required],
    organisationName: ['', Validators.required],
    deviceID: ['', Validators.required],
    userID: [{ value: '', disabled: true }, Validators.required]
  });
    const id = this.route.snapshot.paramMap.get('id');

    if (id) {
      this.isEditMode = true;
      this.vehicleId = +id;
      this.loadVehicle(this.vehicleId);
    }else {
      this.setAutoUserId();
    }
  }

  setAutoUserId() {
    const currentUserId = this.authService.getCurrentUserId(); 
    if (currentUserId) {
      this.vehicleForm.patchValue({ userID: currentUserId });
    }
  }

  loadVehicle(id: number) {
    this.vehicleService.getVehicleById(id).subscribe({
      next: (vehicle) => {
        this.vehicleForm.patchValue(vehicle);
      },
      error: () => alert('Vehicle not found')
    });
  }

  onSubmit() {
    if (this.vehicleForm.invalid) return;

    if (this.isEditMode) {
      this.vehicleService.updateVehicle(this.vehicleId, this.vehicleForm.value)
        .subscribe(() => {
          alert('Vehicle updated successfully');
          this.router.navigate(['/vehicles']);
        });
    } else {
      this.vehicleService.addVehicle(this.vehicleForm.value)
        .subscribe(() => {
          alert('Vehicle added successfully');
          this.router.navigate(['/vehicles']);
        });
    }
  }
}