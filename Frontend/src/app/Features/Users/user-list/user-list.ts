import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UserService } from '../../../Core/Services/user.service';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './user-list.html',
  styleUrls: ['./user-list.css'],
})
export class UserListComponent implements OnInit {

  users: any[] = [];

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.userService.getAllUsers().subscribe({
      next: (data) => {
        this.users = [...data];
        console.log("API Response:", data);
      },
      error: (err) => {
        console.error(err);
        alert('Failed to load users');
      }
    });
  }

  trackById(index: number, user: any): number {
  return user.userID;
}

  // deleteUser(id: number) {
  //   if (confirm('Are you sure you want to delete this user?')) {
  //     this.userService.deleteUser(id).subscribe({
  //       next: () => {
  //         alert('User deleted successfully');
  //         this.loadUsers();
  //       },
  //       error: (err) => {
  //         console.error(err);
  //         alert('Delete failed');
  //       }
  //     });
  //   }
  // }
}