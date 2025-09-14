
# 🪙 CoinKeep API

CoinKeep is a personal finance management API built with **.NET 9**, **Entity Framework Core**, and **SQLite** (for development).
It provides endpoints for managing users, categories, and transactions — helping users track income and expenses in a structured way.

---

## ✨ Features

- 👤 **User Management**: Create and manage user accounts.
- 📂 **Categories**: Organize expenses and income into custom or default categories.
- 💸 **Transactions**: Add, update, and delete transactions linked to categories.
- 🔐 **Authentication** (planned): Secure endpoints using JWT-based authentication.
- 🐳 **Docker Ready**: Easily containerized for deployment with production databases and secrets.

---

## 🏗️ Project Structure

The solution follows a **4-project architecture** for clean separation of concerns:

```

CoinKeep/
├── CoinKeep.Api           # ASP.NET Core Web API project
├── CoinKeep.Core          # Domain models and interfaces
├── CoinKeep.Infrastructure# EF Core DbContext, migrations, repositories
├── CoinKeep.Application   # Business logic and services

````

---

## ⚙️ Requirements

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQLite](https://www.sqlite.org/download.html) (default dev database)
- [Docker](https://docs.docker.com/get-docker/) (optional, for containerized deployment)

---

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/coinkeep.git
cd coinkeep
````

### 2. Apply Database Migrations

```bash
dotnet ef database update --project CoinKeep.Infrastructure --startup-project CoinKeep.Api
```

### 3. Run the API

```bash
dotnet run --project CoinKeep.Api
```

API will be available at:

```
http://localhost:5000
https://localhost:7000
```

---

## 📖 Example Endpoints

### Get all users

```http
GET /user/profile
```

### Create a new transaction

```http
POST /transaction
Content-Type: application/json

{
  "amount": 120.50,
  "description": "Grocery shopping",
  "categoryId": 1,
  "userId": 1
}
```

---

## 🧪 Running in Docker

Build and run the API in a container:

```bash
docker build -t coinkeep-api -f CoinKeep.Api/Dockerfile .
docker run -p 5000:5000 -p 7000:7000 coinkeep-api
```

---

## 🔐 Configuration

Application settings are stored in `appsettings.json`.
For production:

- Use **Docker secrets** or environment variables for sensitive values (JWT keys, DB connection strings).
- Example:

```bash
export ConnectionStrings__DefaultConnection="your-production-db"
export Jwt__Secret="your-secret-key"
```

---

## 📚 Learning Goals

This project is part of a learning journey to explore:

- ✅ Clean Architecture in .NET
- ✅ Entity Framework Core with migrations
- ✅ DTOs and abstraction
- ✅ Authentication and JWT
- ✅ Deployment with Docker

---

## 📜 License

This project is licensed under the **MIT License** – free to use and modify.

---
