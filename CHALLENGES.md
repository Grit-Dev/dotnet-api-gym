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