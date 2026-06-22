# Notes API

REST API for managing notes built with ASP.NET Core Web API.

## Features

* User registration and login
* JWT authentication
* CRUD operations for notes
* PostgreSQL database
* Entity Framework Core
* Fluent Validation
* Sorting notes

## Technologies

* ASP.NET Core Web API
* C#
* Entity Framework Core
* PostgreSQL
* JWT Authentication
* Docker (for PostgreSQL)

## Getting Started

### Prerequisites

Make sure you have installed:

* .NET 8 SDK
* Docker Desktop
* PostgreSQL Docker image

### Clone repository

```bash
git clone https://github.com/YOUR_USERNAME/NotesApi.git
cd NotesApi
```

### Run PostgreSQL

```bash
docker run --name notes-db \
-e POSTGRES_USER=postgres \
-e POSTGRES_PASSWORD=postgres \
-e POSTGRES_DB=notesdb \
-p 5432:5432 \
-d postgres
```

### Configure application

Create `appsettings.json` using `appsettings.example.json` and set your own values.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=notesdb;Username=postgres;Password=your_password"
  },

  "Jwt": {
    "Key": "your-super-secret-key",
    "Issuer": "NotesApi",
    "Audience": "NotesApiUsers",
    "ExpiresMinutes": 60
  }
}
```

### Apply migrations

```bash
dotnet ef database update
```

### Run application

```bash
dotnet run
```

The API will be available at:

```text
https://localhost:xxxx
```

## API Endpoints

### Authentication

| Method | Endpoint    | Description       |
| ------ | ----------- | ----------------- |
| POST   | `/reg` | Register new user |
| POST   | `/log`    | Login user        |

## Authentication

Protected endpoints require JWT token.

Example:

```http
Authorization: Bearer YOUR_TOKEN
```

Use Postman for more efficiency:
<img width="1920" height="976" alt="{BF2E0F46-AFB4-41F1-8A63-DCCBC4A9CF86}" src="https://github.com/user-attachments/assets/ffc7ae86-3090-43f6-a92d-6dd728d3d366" />


