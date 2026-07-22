# Basic REST API Challenges

This file records the practical challenges completed during the Basic REST API project.

Each challenge increases gradually in difficulty. A challenge is complete when the implementation works, the required behaviour has been tested, and the code can be explained.

## Challenge 1 — Return Structured Game Data

**Status:** In progress

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


## Challenge 3 — Create a Game

**Status:** Complete

### Goal

Create an endpoint that accepts game data from the request body, assigns a new identifier, adds the game to the in-memory collection, and returns an appropriate HTTP response.

### Requirements

- Create a `POST /api/games` endpoint.
- Accept a `Game` object from the request body.
- Do not trust the client-provided ID.
- Generate the next available ID on the server.
- Add the new game to the `_games` collection.
- Return `201 Created`.
- Return the newly created game in the response body.
- Include a location pointing to `GET /api/games/{id}`.
- Confirm the new game appears in `GET /api/games`.
- Explain request-body model binding.
- Explain why `201 Created` is more appropriate than `200 OK`.

### Completion Checklist

- [x] Learn how POST and request bodies work
- [x] Create the POST endpoint
- [x] Generate the game ID on the server
- [x] Add the game to the collection
- [x] Return `201 Created`
- [x] Include the created game in the response
- [x] Confirm the game can be retrieved afterward
- [x] Build successfully
- [x] Test through Swagger
- [x] Explain the endpoint behaviour