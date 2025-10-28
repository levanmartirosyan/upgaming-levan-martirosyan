# BOOKCATALOG

Basic RESTful Web API for managing authors and books, written in **.NET 6** using **Entity Framework Core** with an **In-Memory Database**.  
This project follows clean architecture principles and demonstrates clear separation between layers.

---

## REQUIREMENTS
- .NET 6 SDK  
- Visual Studio 2022 or VS Code  

---

## PROJECT STRUCTURE
- **Api** – ASP.NET Core Web API project (controllers, startup, swagger)  
- **Application** – Application layer: DTOs, services, interfaces  
- **Domain** – Domain entities (Author, Book)  
- **Infrastructure** – Data access layer, EF Core repositories  

---

## FEATURES
- Get all authors and books  
- CRUD operations for authors and books  
- In-memory seeded data (auto-created on startup)  

---

## ENDPOINTS

### **AUTHOR** `/api/author`
- `GET /all` – Get all authors  
- `GET /{id}` – Get author by ID  
- `POST /add` – Add new author  
- `PUT /edit` – Update author  
- `DELETE /delete?id={id}` – Delete author  

### **BOOK** `/api/book`
- `GET /all` – Get all books (with optional filters)  
- `GET /{id}` – Get book by ID  
- `GET /author/{authorId}` – Get books by author  
- `POST /add` – Add new book  
- `PUT /edit` – Update book  
- `DELETE /delete?id={id}` – Delete book  

---

## SETUP INSTRUCTIONS

**Clone the repository:**
```bash
git clone https://github.com/levanmartirosyan/upgaming-levan-martirosyan.git

dotnet run
```

The project will start at:

HTTP: http://localhost:5065

HTTPS: https://localhost:7146

Swagger will be available at:
👉 https://localhost:7146/swagger
