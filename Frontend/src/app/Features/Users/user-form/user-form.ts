import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../../Core/Services/user.service';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './user-form.html',
  styleUrls: ['./user-form.css'],
})
export class UserFormComponent implements OnInit {

  userForm: FormGroup;
  isEditMode = false;
  userId!: number;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.userForm = this.fb.group({
      name: ['', Validators.required],
      password: ['', Validators.required],
      mobileNumber: ['', Validators.required],
      organization: [''],
      address: [''],
      emailAddress: ['', [Validators.required, Validators.email]],
      location: [''],
      photoPath: ['']
    });
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if (id) {
      this.isEditMode = true;
      this.userId = +id;

      this.userService.getUserById(this.userId)
        .subscribe({
          next: (user) => {
            this.userForm.patchValue(user);
          },
          error: (err) => {
            console.error(err);
            alert('Failed to load user');
          }
        });
    }
  }

  onSubmit() {
    if (this.userForm.valid) {

      if (this.isEditMode) {
        // UPDATE
        this.userService.updateUser(this.userId, this.userForm.value)
          .subscribe({
            next: () => {
              alert('User updated successfully!');
              this.router.navigate(['/users']);
            },
            error: (err) => {
              console.error(err);
              alert('Update failed');
            }
          });

      } else {
        // CREATE
        this.userService.createUser(this.userForm.value)
          .subscribe({
            next: () => {
              alert('User created successfully!');
              this.router.navigate(['/users']);
            },
            error: (err) => {
              console.error(err);
              alert('Creation failed');
            }
          });
      }
    }
  }
}