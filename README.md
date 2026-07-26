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
- Service-layer patterns
- EF Core and SQL Server
- Entity relationships
- Authentication and authorization
- JWT authentication
- User-owned data
- Role-based access control
- Unit testing
- Integration testing
- Logging and error handling
- Clean backend structure

This repository is used for focused learning, repetition, and technical interview preparation.

It complements my larger portfolio project, **Cyberpunk Vault**, a Cyberpunk TCG collection, wishlist, and trading API. Cyberpunk Vault remains the main portfolio application, while the API Gym provides smaller projects where backend concepts can be practised repeatedly and understood in isolation.

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

Exercises include:

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
- Answering interview-style questions about completed work

Difficulty increases gradually as understanding improves.

When a concept is not yet comfortable, progression pauses. The concept is revisited through smaller examples, recall questions, debugging tasks, endpoint extensions, and unit-test challenges until it can be implemented and explained with confidence.

New topics are not introduced simply because a planned folder has been reached. Progression is based on demonstrated understanding.

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

AI-powered inline code prediction and automatic code completion are disabled during learning exercises.

Code and tests are typed and worked through manually so that the underlying patterns are understood rather than accepted through automatic prediction.

When a concept is completely new, a small guided example may be provided first. Similar endpoints, extensions, debugging tasks, and test scenarios are then completed with less assistance.

The aim is to become capable of explaining, implementing, testing, and debugging each feature without relying on automatically generated solutions.

## Repository Structure

```text
dotnet-api-gym
│
├── 01-basic-rest-api
├── BasicRestApi.Tests
├── 02-ef-core-relationships
├── 03-jwt-auth-user-owned-data
├── 04-roles-and-admin-access
├── 05-private-messaging-api
├── 06-refresh-tokens
├── 07-service-layer-and-clean-architecture-basics
├── CHALLENGES.md
├── DotnetApiGym.sln
└── README.md
```

New projects will be added as the concepts in the previous project become comfortable.

## Projects

### 01 — Basic REST API

**Status:** In progress

The first project introduces the structure, behaviour, architecture, and testing of a controller-based ASP.NET Core Web API.

Implemented topics include:

- Creating a .NET solution
- Creating a controller-based ASP.NET Core Web API
- Understanding `Program.cs` and the application entry point
- Registering and mapping controllers
- Attribute routing
- HTTP methods
- HTTP status codes
- `IActionResult`
- `ActionResult<T>`
- Structured models
- Request DTOs
- Response DTOs
- Request validation
- Automatic validation responses with `[ApiController]`
- Complete CRUD operations
- Dependency injection
- Service interfaces
- Service implementations
- Separating controller and service responsibilities
- Singleton, scoped, and transient service lifetimes
- xUnit test projects
- Arrange, Act, Assert
- `[Fact]` and `[Theory]`
- Testing successful and unsuccessful service behaviour
- Test isolation
- Running builds and tests through Visual Studio and the .NET CLI
- OpenAPI and Swagger UI

#### Current Endpoints

```http
GET    /api/games
GET    /api/games/{id}
POST   /api/games
PUT    /api/games/{id}
DELETE /api/games/{id}
```

#### Example Response

```json
{
  "id": 2,
  "title": "Cyberpunk 2077",
  "genre": "Action RPG",
  "releaseYear": 2020
}
```

#### Current Architecture

```text
HTTP request
    ↓
GamesController
    ↓
IGameService
    ↓
GameService
    ↓
In-memory game collection
```

The controller is responsible for:

- Receiving HTTP input
- Mapping request DTOs into models
- Calling the service
- Mapping models into response DTOs
- Returning the appropriate HTTP status code

The service is responsible for:

- Managing the in-memory game collection
- Retrieving games
- Generating IDs
- Creating games
- Updating games
- Deleting games

#### HTTP Responses

The current endpoints use the following status codes:

```text
200 OK           Request succeeded and response data is returned
201 Created      A new game was successfully created
204 No Content   An update or deletion succeeded without a response body
400 Bad Request  Request validation failed
404 Not Found    A game with the requested ID does not exist
```

#### Current Test Coverage

`GameService` unit tests currently cover:

- Returning the seeded games
- Confirming each expected seeded game is present
- Retrieving an existing game
- Returning `null` for a missing game
- Creating a game
- Generating a new game ID
- Preserving submitted game properties
- Confirming a created game is stored
- Updating an existing game
- Confirming updated properties are stored
- Returning `false` when updating a missing game
- Deleting an existing game
- Confirming a deleted game can no longer be retrieved
- Returning `false` when deleting a missing game

#### Upcoming Work

- Unit-test `GamesController`
- Mock `IGameService`
- Test controller HTTP result types
- Test successful and unsuccessful controller responses
- Add API integration tests
- Build another controller from written acceptance criteria
- Complete extension and debugging challenges

### 02 — EF Core Relationships

**Status:** Planned

Planned topics:

- `DbContext`
- `DbSet`
- Entities
- Primary keys
- Foreign keys
- One-to-many relationships
- Database migrations
- SQL Server
- Asynchronous EF Core operations
- `async` and `await`
- Cancellation tokens
- Loading related data
- Tracking and `AsNoTracking`
- Database testing decisions

### 03 — JWT Authentication and User-Owned Data

**Status:** Planned

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

**Status:** Planned

Planned topics:

- User and Admin roles
- Role claims
- Role-based authorization
- Admin-only endpoints
- `401 Unauthorized`
- `403 Forbidden`
- Authorization testing

### 05 — Private Messaging API

**Status:** Planned

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

**Status:** Planned

Planned topics:

- Access-token expiration
- Refresh-token generation
- Refresh-token storage
- Token rotation
- Token revocation
- Security considerations
- Refresh-token testing

### 07 — Service Layer and Clean Architecture Basics

**Status:** Planned

This project will expand on the service-layer fundamentals introduced in the Basic REST API.

Planned topics:

- Controller responsibilities
- Application services
- Interfaces
- Dependency injection
- Separation of concerns
- Mocking dependencies
- Repository patterns
- Unit tests
- Integration tests
- Cleaner backend organisation
- Centralised error handling
- Logging
- Configuration
- Maintainable project boundaries

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

Run all tests:

```bash
dotnet test DotnetApiGym.sln
```

Run only the `GameServiceTests` class:

```bash
dotnet test DotnetApiGym.sln --filter "FullyQualifiedName~BasicRestApi.Tests.Services.GameServiceTests"
```

## Local API Documentation

Swagger UI is available while the API is running in the Development environment:

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
- [x] Returned structured game data using `200 OK`
- [x] Added OpenAPI generation and Swagger UI
- [x] Added an endpoint for retrieving a game by ID
- [x] Returned `404 Not Found` when a game does not exist
- [x] Added complete CRUD endpoints
- [x] Added request DTOs
- [x] Added response DTOs
- [x] Added request validation
- [x] Returned `201 Created` after creating a game
- [x] Returned `204 No Content` after successful updates and deletions
- [x] Extracted game logic into `GameService`
- [x] Added the `IGameService` abstraction
- [x] Registered the game service with dependency injection
- [x] Injected `IGameService` into `GamesController`
- [x] Learned singleton, scoped, and transient service lifetimes
- [x] Added the first xUnit test project
- [x] Learned Arrange, Act, Assert
- [x] Used `[Fact]` and `[Theory]`
- [x] Added unit tests for successful and unsuccessful `GameService` behaviour
- [x] Confirmed all current tests pass
- [ ] Add unit tests for `GamesController`
- [ ] Mock `IGameService`
- [ ] Add integration tests for the Games API
- [ ] Build a second controller from written acceptance criteria
- [ ] Replace the in-memory collection with EF Core