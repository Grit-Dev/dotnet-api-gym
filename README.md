# .NET API Gym

A progressive C#/.NET learning repository focused on strengthening practical backend development skills through a series of small ASP.NET Core Web API projects.

The projects begin with the fundamentals and gradually increase in difficulty. Each stage builds on the previous one through guided learning, implementation challenges, debugging exercises, testing, review, and clean Git commits.

## Purpose

The purpose of this repository is to build confidence and practical understanding in:

- ASP.NET Core Web APIs
- Controllers and routing
- HTTP methods and status codes
- `IActionResult` and `ActionResult<T>`
- Models and DTOs
- Request validation
- Dependency injection
- EF Core and SQL Server
- Entity relationships
- Authentication and authorization
- JWT authentication
- User-owned data
- Role-based access control
- Unit testing
- Integration testing
- Service-layer patterns
- Logging and error handling
- Clean backend structure

This repository is used for focused learning, repetition, and technical interview preparation.

It complements my larger portfolio project, **Cyperpunk Vault**, a Cyberpunk TCG collection, wishlist, and trading API. Cyberpunk Vault remains the main portfolio application, while the API Gym provides smaller projects where backend concepts can be practised repeatedly and understood in isolation.

## Learning Approach

Each project follows a progressive learning cycle:

1. Learn a new concept in plain English.
2. Understand why the concept exists and where it belongs.
3. Review a small practical example.
4. Build a guided implementation.
5. Complete implementation challenges with reduced guidance.
6. Complete debugging and code-reading exercises.
7. Write unit tests for successful and unsuccessful scenarios.
8. Review the implementation and tests.
9. Run the complete solution using `dotnet build` and `dotnet test`.
10. Commit each working milestone using a clear Git commit message.
11. Apply the patterns learned here to larger applications such as Cyberpunk Vault.

The goal is not simply to complete each project. The goal is to understand the complete request flow and become capable of designing, implementing, testing, debugging, and explaining backend features independently.

## Challenge-Based Progression

Challenges are a core part of this repository.

Exercises will include:

- Completing missing controller actions
- Building endpoints from written requirements
- Predicting what code or an HTTP request will return
- Finding and fixing broken code
- Choosing appropriate HTTP status codes
- Designing DTOs
- Adding validation rules
- Writing successful and unsuccessful test cases
- Explaining implementation decisions
- Refactoring working code into a cleaner structure
- Answering interview-style questions about the completed work

Difficulty will increase gradually as understanding improves.

When a concept is not yet comfortable, progression will pause. The concept will be revisited through smaller examples, recall questions, debugging tasks, endpoint extensions, and unit-test challenges until it can be implemented and explained with confidence.

New topics will not be introduced simply because a planned folder has been reached. Progression is based on demonstrated understanding.

## Learning and AI Usage

AI is used in this repository as a tutor, challenge setter, quizzer, debugging partner, and code-review assistant.

It is used to:

- Explain new C# and ASP.NET Core concepts
- Provide progressively harder implementation challenges
- Ask recall and interview-style questions
- Review code and unit tests
- Identify bugs and explain corrections
- Suggest improvements to structure, security, and testing
- Reinforce topics that are not yet fully understood

AI-powered inline code prediction and automatic code completion are disabled during the learning exercises.

Code and tests are typed and worked through manually so that the underlying patterns are understood rather than accepted through automatic prediction.

When a concept is completely new, a small guided example may be provided first. Similar endpoints, extensions, debugging tasks, and test scenarios are then completed with less assistance.

The aim is to become capable of explaining, implementing, testing, and debugging each feature without relying on automatically generated solutions.

## Planned Repository Structure

```text
dotnet-api-gym
│
├── 01-basic-rest-api
├── 02-ef-core-relationships
├── 03-jwt-auth-user-owned-data
├── 04-roles-and-admin-access
├── 05-private-messaging-api
├── 06-refresh-tokens
├── 07-service-layer-and-clean-architecture-basics
└── README.md
```

New projects will be added as the concepts in the previous project become comfortable.

## Projects

### 01 — Basic REST API

**Status:** In progress

The first project introduces the structure and behaviour of a controller-based ASP.NET Core Web API.

Current topics include:

- Creating a .NET solution
- Creating a controller-based ASP.NET Core Web API
- Understanding the purpose of `Program.cs`
- Understanding the application entry point
- Registering controller support
- Mapping controller routes
- Attribute routing
- Creating a controller
- Handling an HTTP `GET` request
- Understanding `IActionResult`
- Understanding `ActionResult<T>`
- Returning `200 OK`
- Returning JSON data
- Understanding OpenAPI
- Configuring Swagger UI
- Understanding `launchSettings.json`
- Running and building through Visual Studio and the .NET CLI

Current endpoint:

```http
GET /api/games
```

Example response:

```json
[
  "Cyberpunk 2077",
  "The Witcher 3",
  "Crimson Desert"
]
```

Upcoming work:

- Replace plain strings with a structured `Game` model
- Retrieve a game by ID
- Use route parameters
- Return `404 Not Found` when a game does not exist
- Add `POST`, `PUT`, and `DELETE` endpoints
- Introduce request and response DTOs
- Add validation
- Create an xUnit test project
- Learn Arrange, Act, Assert
- Write controller unit tests
- Test successful and unsuccessful responses
- Complete extension and debugging challenges

### 02 — EF Core Relationships

Planned topics:

- `DbContext`
- Entities
- Primary keys
- Foreign keys
- One-to-many relationships
- Database migrations
- SQL Server
- Asynchronous EF Core operations
- Loading related data
- Database testing decisions

### 03 — JWT Authentication and User-Owned Data

Planned topics:

- User registration
- Password hashing
- Login
- JWT creation
- Claims
- Protected endpoints
- Accessing the current user
- Ensuring users can only access their own records
- Authentication testing
- Ownership and security checks

### 04 — Roles and Admin Access

Planned topics:

- User and Admin roles
- Role claims
- Role-based authorization
- Admin-only endpoints
- Forbidden and unauthorized responses
- Authorization testing

### 05 — Private Messaging API

Planned topics:

- Conversations
- Messages
- Senders and recipients
- Entity relationships
- Conversation membership
- Access checks
- Private user data
- Messaging test scenarios

### 06 — Refresh Tokens

Planned topics:

- Access-token expiration
- Refresh-token generation
- Refresh-token storage
- Token rotation
- Token revocation
- Security considerations
- Refresh-token testing

### 07 — Service Layer and Clean Architecture Basics

Planned topics:

- Controller responsibilities
- Service classes
- Interfaces
- Dependency injection
- Separation of concerns
- Mocking dependencies
- Unit tests
- Integration tests
- Cleaner backend organisation
- Centralised error handling
- Logging

## Development Requirements

- .NET 9 SDK
- Visual Studio 2022 or another compatible editor
- Git
- SQL Server
- SQL Server Management Studio for later projects

## Development Commands

Build the complete solution:

```bash
dotnet build DotnetApiGym.sln
```

Run the Basic REST API:

```bash
dotnet run --project ./01-basic-rest-api/BasicRestApi.csproj --launch-profile https
```

Run all tests once test projects have been introduced:

```bash
dotnet test DotnetApiGym.sln
```

## Local API Documentation

Swagger UI is available while running in the Development environment:

```text
https://localhost:7123/swagger
```

The port may differ depending on the local launch settings.

Swagger UI provides one page for viewing and testing the endpoints exposed by all mapped controllers.

## Current Progress

- [x] Created the .NET solution and Basic REST API project
- [x] Confirmed the solution builds and runs successfully
- [x] Reviewed the application startup flow in `Program.cs`
- [x] Created `GET /api/games`
- [x] Added a structured `Game` model
- [x] Updated the endpoint to return `ActionResult<List<Game>>`
- [x] Returned structured game data using `200 OK`
- [x] Added OpenAPI generation and Swagger UI
- [x] Completed the first structured-data challenge
- [x] Add an endpoint for retrieving a game by ID
- [x] Return `404 Not Found` when a game does not exist
- [x] Add complete CRUD endpoints
- [x] Introduce DTOs and validation
- [x] Extract game logic into a service
- [x] Use dependency injection in the games controller
- [ ] Add the first xUnit test project
- [ ] Write and run the first unit tests