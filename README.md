# .NET API Gym

![Status](https://img.shields.io/badge/status-completed-success)
![.NET](https://img.shields.io/badge/.NET-9-purple)
![Tests](https://img.shields.io/badge/tests-passing-success)

A progressive C#/.NET learning repository focused on strengthening practical backend development skills through a series of small ASP.NET Core Web API projects.

The projects begin with fundamentals and gradually increase in difficulty. Each stage builds on the previous one through guided learning, implementation challenges, debugging exercises, testing, review, and clean Git commits.

The goal is not simply to complete projects. The goal is to understand how backend applications are designed, how requests flow through an API, how data is stored, how problems are debugged, and how features can be explained independently.

---

# Purpose

This repository exists to build confidence and practical understanding in:

- ASP.NET Core Web APIs
- Controllers and routing
- HTTP methods and status codes
- IActionResult and ActionResult<T>
- Models and DTOs
- Request and response design
- Validation
- Dependency injection
- Service-layer patterns
- Entity Framework Core
- Database relationships
- SQLite databases
- Unit testing
- Integration testing
- Clean backend structure
- Debugging backend applications

This repository supports my larger portfolio project, **Cyberpunk Vault**, by allowing backend concepts to be practised in smaller isolated projects before applying them to a larger application.

---

# Learning Approach

Each project follows a progressive cycle:

1. Learn a new concept in plain English.
2. Understand why the concept exists.
3. Review a small practical example.
4. Implement the feature with guidance.
5. Complete challenges with reduced guidance.
6. Debug broken implementations.
7. Write tests for successful and unsuccessful scenarios.
8. Review design decisions.
9. Build and test the complete solution.
10. Commit completed milestones with meaningful Git messages.

The goal is to become capable of:

- Designing API features
- Implementing backend logic
- Understanding existing code
- Debugging issues
- Writing tests
- Explaining technical decisions

---

# Project 01 — Basic REST API

**Status: Completed**

The first project introduced the foundations of ASP.NET Core Web API development.

The project started as a simple in-memory API and was expanded into a database-backed application using Entity Framework Core.

---

# Implemented Concepts

## ASP.NET Core API Fundamentals

- Creating a .NET Web API project
- Understanding Program.cs
- Controller registration
- Attribute routing
- HTTP methods
- HTTP status codes
- Swagger / OpenAPI

## API Design

- Request DTOs
- Response DTOs
- Model separation
- Validation
- Mapping between models and DTOs
- Returning correct HTTP responses

## Architecture

Implemented separation between:

- Controllers
- Interfaces
- Services
- Database layer

The controller handles HTTP concerns.

The service layer handles application behaviour.

The database layer handles persistence.

---

# Current Architecture

```text
HTTP Request
      |
      ↓
GamesController
      |
      ↓
IGameService
      |
      ↓
GameService
      |
      ↓
GameDbContext
      |
      ↓
SQLite Database
```

---

# Database Features

Implemented Entity Framework Core:

- DbContext
- DbSet
- Database persistence
- Foreign keys
- Navigation properties
- Entity relationships
- Loading related data using Include()

Relationships implemented:

## Developer → Games

One developer can have multiple games.

```text
Developer
    |
    |
    ├── Game
    ├── Game
    └── Game
```

## Games ↔ Platforms

Games can exist on multiple platforms.

```text
Game
 |
 ├── PlayStation 5
 |
 └── Xbox Series X
```

---

# API Endpoints

## Get all games

```http
GET /api/games
```

## Get game by ID

```http
GET /api/games/{id}
```

## Create game

```http
POST /api/games
```

## Update game

```http
PUT /api/games/{id}
```

## Delete game

```http
DELETE /api/games/{id}
```

---

# Example Response

```json
{
  "id": 2,
  "title": "Cyberpunk 2077",
  "genre": "Action RPG",
  "releaseYear": 2020,
  "developerId": 1,
  "developerName": "CD PROJEKT",
  "platforms": [
    "PlayStation 5",
    "Xbox Series X"
  ]
}
```

---

# Testing

Testing was implemented using xUnit.

## Unit Tests

Covered:

- Retrieving existing games
- Handling missing games
- Creating games
- Updating games
- Deleting games
- Handling unsuccessful operations

## Controller Tests

Implemented using Moq.

Covered:

- Correct HTTP responses
- Successful controller actions
- Failed controller actions
- Mocking service dependencies

## Integration Tests

Implemented using:

- WebApplicationFactory
- Test database setup
- In-memory database replacement

Covered:

- Full HTTP request flow
- Creating resources
- Updating resources
- Deleting resources
- Confirming API behaviour

---

# HTTP Responses Used

```text
200 OK
Request succeeded and data returned

201 Created
Resource successfully created

204 No Content
Update or delete completed successfully

400 Bad Request
Invalid request data

404 Not Found
Requested resource does not exist
```

---

# Development Requirements

- .NET 9 SDK
- Visual Studio 2022 or compatible editor
- Git
- SQLite

---

# Running The Project

Build solution:

```bash
dotnet build
```

Run API:

```bash
dotnet run
```

Run tests:

```bash
dotnet test
```

---

# Reflection

This project started as a simple REST API and gradually evolved into a database-backed application.

The biggest lessons learned:

- Controllers should focus on HTTP handling rather than business logic.
- Services provide a cleaner location for application behaviour.
- DTOs prevent directly exposing database models.
- Entity Framework relationships require understanding both foreign keys and navigation properties.
- Unit tests and integration tests solve different problems.
- Debugging database issues requires understanding the complete request flow.

The project is intentionally kept smaller than a production application so that concepts can be understood clearly before being applied to larger systems.

---

# Future Learning Projects

## Project 02 — Advanced EF Core Relationships

Topics:

- Advanced relationships
- Migrations
- SQL Server
- Async EF Core
- Tracking and AsNoTracking
- Database design decisions

---

## Project 03 — Authentication API

Topics:

- User registration
- Password hashing
- JWT authentication
- Claims
- Protected endpoints
- User-owned data

---

## Project 04 — Roles and Permissions

Topics:

- User roles
- Admin access
- Authorization
- Security testing

---

# Current Progress

- [x] Created .NET solution
- [x] Created ASP.NET Core Web API
- [x] Built CRUD endpoints
- [x] Added DTOs
- [x] Added validation
- [x] Added dependency injection
- [x] Added service layer
- [x] Added interfaces
- [x] Added Entity Framework Core
- [x] Added SQLite database
- [x] Added Developer relationship
- [x] Added Platform relationship
- [x] Added controller unit tests
- [x] Added service unit tests
- [x] Added integration tests
- [x] Completed API testing workflow

---

# Completed Challenge

## Database-backed Games API

Completed:

- CRUD functionality
- Service layer
- Database persistence
- Entity relationships
- Testing
- API integration testing
