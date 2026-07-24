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
