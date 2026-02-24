# 🚗 Vehicle Tracking System

A full-stack Vehicle Tracking System built using **ASP.NET Core Web API (.NET 9)** and **Angular (Standalone Components)** with secure **JWT Authentication**.

This project was developed as part of a technical interview assignment to demonstrate:

- Clean architecture
- Secure authentication
- Route protection
- Full-stack integration
- Production-style application flow

---

# 🏗 Tech Stack

## 🔹 Backend
- ASP.NET Core Web API (.NET 9)
- Entity Framework Core
- SQL Server
- JWT Authentication
- BCrypt Password Hashing
- Swagger with Bearer Authentication
- Layered Architecture (Controller → Service → DTO → DbContext)

## 🔹 Frontend
- Angular (Standalone Components)
- Reactive Forms
- Angular Router
- HTTP Interceptors
- Route Guards
- JWT Token Handling

---

# 🔐 Authentication Flow

1. User registers
2. User logs in
3. Backend generates JWT
4. Frontend stores token in `localStorage`
5. HTTP Interceptor attaches `Authorization: Bearer <token>`
6. Route Guard protects restricted routes
7. Logout clears token

---

# 📂 Application Routes

## Public
- `/login`
- `/register`

## Protected
- `/vehicles`
- `/vehicles/add`
- `/vehicles/edit/:id`

All vehicle APIs are protected using `[Authorize]`.

---

# 🚘 Vehicle Features

- Create Vehicle
- Edit Vehicle
- View Vehicle List
- Search Vehicles
- Pagination
- JWT-secured endpoints

---

# 👤 User Features

- Register new account
- Login with JWT
- Upload profile image
- Secure logout
- Protected dashboard access

---

# 🖼 User Image Upload

- Users can upload a profile image during registration
- Images are stored on the server (`wwwroot/uploads`)
- Only image file types are accepted
- Image path is stored in database
- Accessible via static file middleware

---

# 🧱 Backend Architecture
Controller -> Services -> DTOs -> DBContext (EF Core)


### Design Principles

- Dependency Injection
- Separation of Concerns
- Secure Token Validation
- DTO-based communication
- Scoped Services

---

# 🔑 JWT Security

- Issuer validation
- Audience validation
- Signing key validation
- Token expiration handling
- Swagger configured for Bearer testing

Example Header:
Authorization: Bearer <JWT_TOKEN>


---

# 🚀 Running the Project

## Backend

dotnet restore
dotnet run

## Frontend

npm install
ng serve

Author

Kartik Jachak
Full-Stack Developer (ASP.NET + Angular)
