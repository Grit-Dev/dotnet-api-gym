# Basic REST API Challenges

This file records the practical challenges completed during the Basic REST API project.

Each challenge increases gradually in difficulty. A challenge is complete when the implementation works, the required behaviour has been tested, and the code can be explained.

## Challenge 1 — Return Structured Game Data

**Status:** In Completed

### Goal

Replace the current list of game-title strings with structured `Game` objects.

### Requirements

- Create a `Game` model.
- Give the model an identifier, title, genre, and release year.
- Change `GetGames()` to return `ActionResult<List<Game>>`.
- Create at least three `Game` objects.
- Give each game realistic property values.
- Return the collection using `200 OK`.
- Confirm the JSON response through Swagger UI.
- Explain why structured objects are more useful than plain strings.
- Explain why `ActionResult<List<Game>>` is clearer than `IActionResult`.

### Completion Checklist

- [x] Create the `Models` folder
- [x] Create the `Game` model
- [x] Update the controller return type
- [x] Populate a `List<Game>`
- [x] Build the solution successfully
- [x] Test the endpoint through Swagger UI
- [x] Explain the model and endpoint behaviour


##
## Challenge 2 — Retrieve a Game by ID
##

**Status:** Complete

### Goal

Create an endpoint that retrieves one game using its unique identifier.

### Requirements

- Create a `GET /api/games/{id}` endpoint.
- Accept the game ID as a route parameter.
- Return `ActionResult<Game>`.
- Search the existing game collection for the requested ID.
- Return `200 OK` with the matching game.
- Return `404 Not Found` when no matching game exists.
- Test an existing ID through Swagger UI.
- Test a missing ID through Swagger UI.
- Explain why the endpoint returns `ActionResult<Game>`.
- Explain why a missing resource should return `404` rather than `200` with `null`.

### Completion Checklist

- [x] Learn how route parameters work
- [x] Make the games collection reusable
- [x] Create the endpoint
- [x] Return the matching game
- [x] Handle a missing game
- [x] Build successfully
- [x] Test successful and unsuccessful requests
- [x] Explain the endpoint behaviour

## Challenge 4 — Update a Game

**Status:** Complete

### Goal

Update an existing game using its route ID and replacement data from the request body.

### Requirements

- Create `PUT /api/games/{id}`.
- Accept the game ID from the route.
- Accept updated game data from the JSON request body.
- Find the existing game using the route ID.
- Return `404 Not Found` if the game does not exist.
- Do not allow the request body to change the game’s ID.
- Update the title, genre, and release year.
- Return `204 No Content` after a successful update.
- Confirm the updated game using `GET /api/games/{id}`.

### Completion Checklist

- [x] Create the PUT endpoint
- [x] Bind the route ID
- [x] Bind the request body
- [x] Find the existing game
- [x] Handle a missing game
- [x] Update the correct properties
- [x] Protect the existing ID
- [x] Return `204 No Content`
- [x] Build successfully
- [x] Test through Swagger
- [x] Explain the endpoint behaviour

## Challenge 5 — Delete a Game

**Status:** Complete

### Goal

Delete an existing game using its unique identifier.

### Requirements

- Create `DELETE /api/games/{id}`.
- Accept the ID from the route.
- Search for the matching game.
- Return `404 Not Found` when the game does not exist.
- Remove the matching game from `_games`.
- Return `204 No Content` after a successful deletion.
- Confirm the deleted game no longer appears in `GET /api/games`.
- Confirm `GET /api/games/{id}` returns `404` afterward.
- Explain why `IActionResult` is suitable for this endpoint.
- Explain why `204 No Content` is appropriate.

### Completion Checklist

- [x] Create the DELETE endpoint
- [x] Bind the route ID
- [x] Find the existing game
- [x] Handle a missing game
- [x] Remove the game
- [x] Return `204 No Content`
- [x] Build successfully
- [x] Test a successful deletion
- [x] Test a missing ID
- [x] Confirm the deleted game cannot be retrieved
- [x] Explain the endpoint behaviour

## Challenge 6 — Validate Game Requests

**Status:** Complete

### Goal

Prevent invalid game data from being accepted when creating or updating games.

Use separate request DTOs so the client only sends the properties required by each endpoint.

### Endpoints Being Updated

- `POST /api/games`
- `PUT /api/games/{id}`

The GET and DELETE endpoints do not need to change.

### Request DTOs

Create a `Dtos` folder containing:

- `CreateGameRequest.cs`
- `UpdateGameRequest.cs`

`CreateGameRequest` will be used by:

``http
POST /api/games


### ACS

- [x] Create the `Dtos` folder
- [x] Create `CreateGameRequest`
- [x] Create `UpdateGameRequest`
- [x] Require the title
- [x] Restrict the title to 100 characters
- [x] Require the genre
- [x] Restrict the genre to 50 characters
- [x] Restrict the release year to 1950–2100
- [x] Update POST to accept `CreateGameRequest`
- [x] Map the create request into a new `Game`
- [x] Confirm the server generates the game ID
- [x] Update PUT to accept `UpdateGameRequest`
- [x] Map the update request onto the stored game
- [x] Confirm PUT does not change the game ID
- [x] Test invalid POST requests
- [x] Test invalid PUT requests
- [x] Confirm invalid requests return `400 Bad Request`
- [x] Confirm invalid POST requests do not create a game
- [x] Confirm invalid PUT requests do not modify a game
- [x] Confirm valid POST returns `201 Created`
- [x] Confirm valid PUT returns `204 No Content`
- [x] Confirm missing PUT target returns `404 Not Found`
- [x] Build successfully
- [x] Explain how validation attributes work
- [x] Explain how `[ApiController]` handles invalid models


## Challenge 7 — Game Response DTOs and Mapping

**Status:** Complete

### Goal

Stop returning the internal `Game` model directly from the API.

Create a response DTO that defines exactly what game data clients receive.

### Why This Matters

The `Game` model represents data stored inside the application.

A response DTO represents data deliberately exposed by the API.

Separating them allows the internal model to change without automatically changing the public API response.

### Endpoints Being Updated

- `GET /api/games`
- `GET /api/games/{id}`
- `POST /api/games`

The PUT and DELETE endpoints still return `204 No Content`, so they do not need a response DTO.

### Response DTO

Create:

``text
Dtos/GameResponse.cs


### Challenge 8 — Extract a Game Service and Use Dependency Injection

**Status:** Complete

### Goal

Move game storage and CRUD logic out of `GamesController` and into a dedicated service.

Use ASP.NET Core dependency injection to provide the service to the controller.

### New Files

Create a `Services` folder containing:

- `IGameService.cs`
- `GameService.cs`

### Service Interface

Create an `IGameService` interface with operations for:

- Getting all games
- Getting one game by ID
- Creating a game
- Updating a game
- Deleting a game

Suggested method signatures:

``csharp
IReadOnlyList<Game> GetGames();

Game? GetGameById(int id);

Game CreateGame(Game game);

bool UpdateGame(int id, Game game);

bool DeleteGame(int id);


## Challenge 9 — Unit-Test GameService

**Status:** Complete

### Goal

Learn how to test service behaviour automatically using xUnit.

Each test should create a fresh `GameService`, perform one operation, and verify the result.

### Concepts

- Test project
- xUnit
- `[Fact]`
- Arrange, Act, Assert
- Test isolation
- Clear test naming
- Success and failure paths

### Service Behaviours to Test

- Getting all games
- Getting an existing game by ID
- Getting a missing game by ID
- Creating and storing a game
- Generating a game ID
- Updating an existing game
- Attempting to update a missing game
- Deleting an existing game
- Attempting to delete a missing game

### Completion Checklist

- [x] Create the xUnit test project
- [x] Add the test project to the solution
- [x] Reference the API project
- [x] Explain Arrange, Act, Assert
- [x] Test getting an existing game
- [x] Test getting a missing game
- [x] Test getting all games
- [x] Test creating a game
- [x] Confirm creation generates an ID
- [x] Confirm creation stores the game
- [x] Test updating an existing game
- [x] Test updating a missing game
- [x] Test deleting an existing game
- [x] Test deleting a missing game
- [x] Confirm every test has isolated state
- [x] Run all tests successfully


## Challenge 10 — Unit-Test GamesController

**Status:** In progress

### Goal

Learn how to unit-test controller behaviour by replacing the real `IGameService` with a mocked dependency.

The controller tests should verify that service outcomes are translated into the correct HTTP results and response DTOs.

### Concepts

- Controller unit testing
- Mocking dependencies
- `Mock<IGameService>`
- Configuring mock behaviour with `Setup`
- Returning test data with `Returns`
- `OkObjectResult`
- `NotFoundResult`
- `CreatedAtActionResult`
- `NoContentResult`
- Inspecting response DTOs
- Verifying service calls
- Arrange, Act, Assert

### Controller Behaviours to Test

- GET all returns `200 OK`
- GET all returns mapped `GameResponse` objects
- GET by ID returns `200 OK` when the game exists
- GET by ID returns `404 Not Found` when the game does not exist
- POST returns `201 Created`
- POST returns the created response DTO
- POST uses the generated ID in the route values
- PUT returns `204 No Content` when successful
- PUT returns `404 Not Found` when the game does not exist
- DELETE returns `204 No Content` when successful
- DELETE returns `404 Not Found` when the game does not exist

### Completion Checklist

- [x] Install Moq
- [x] Create `GamesControllerTests`
- [x] Explain why the real `GameService` is not used
- [x] Create a mock `IGameService`
- [x] Construct `GamesController` with the mock
- [x] Test GET all
- [x] Test GET by ID success
- [x] Test GET by ID failure
- [x] Test POST
- [x] Test PUT success
- [x] Test PUT failure
- [x] Test DELETE success
- [x] Test DELETE failure
- [x] Verify DTO mapping
- [x] Verify service calls
- [x] Run all tests successfully

## Latest completed challenge

### Challenge 13: Add a Developer to Games ✅

Added developer information throughout the full API workflow.

#### What changed

- Added a required `Developer` string property to games
- Added validation preventing null, empty, whitespace, or values longer than 100 characters
- Updated create and update requests
- Updated game responses
- Updated controller mapping
- Updated EF Core persistence
- Added and applied an EF Core migration
- Updated service unit tests
- Updated controller unit tests
- Updated API integration tests
- Verified that all tests pass

Example request:

```json
{
  "title": "Cyberpunk 2077",
  "genre": "Action RPG",
  "releaseYear": 2020,
  "developer": "CD Projekt"
}