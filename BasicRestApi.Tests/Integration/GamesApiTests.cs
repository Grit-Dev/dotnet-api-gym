using BasicRestApi.Dtos;
using System.Net;
using System.Net.Http.Json;

namespace BasicRestApi.Tests.Integration
{
    // Starts the real api in memory WAF
    public class GamesApiTests : IClassFixture<CustomWebApplicationFactory>
    {
        // Sends a real HTTP Request
        private readonly HttpClient _httpClient;

        public GamesApiTests(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task GetGames_ReturnsOk()
        {
            var response = await _httpClient.GetAsync("/api/games");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetGames_ReturnsOkWithGames()
        {
            var response = await _httpClient.GetAsync("/api/games");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var games = await response.Content.ReadFromJsonAsync<List<GameResponse>>();

            Assert.NotNull(games);
            Assert.NotEmpty(games);
        }

        [Fact]
        public async Task GetGames_ReturnsNotFound_WhenGameDoesNotExist()
        {
            var response = await _httpClient.GetAsync("/api/games/999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetGamesById_WhenGameExists_ReturnsOkWithGame()
        {
            var allGamesFromResponse = await _httpClient.GetAsync("/api/games");

            var games = await allGamesFromResponse.Content.ReadFromJsonAsync<List<GameResponse>>();

            Assert.NotNull(games);
            Assert.NotEmpty(games);

            var expectedGame = games[0];

            var response = await _httpClient.GetAsync($"/api/games/{expectedGame.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var actualGame = await response.Content.ReadFromJsonAsync<GameResponse>();

            Assert.NotNull(actualGame);
            Assert.Equal(expectedGame.Id, actualGame.Id);
            Assert.Equal(expectedGame.Title, actualGame.Title);
            Assert.Equal(expectedGame.Genre, actualGame.Genre);
            Assert.Equal(expectedGame.ReleaseYear, actualGame.ReleaseYear);
            Assert.Equal(expectedGame.Developer, actualGame.Developer);
        }

        [Fact]
        public async Task CreateGame_WithValidRequest_ReturnsCreated()
        {
            var request = new CreateGameRequest
            {
                Title = "I hope you work",
                Genre = "Hail Mary",
                ReleaseYear = 2026
            };

            var respone = await _httpClient.PostAsJsonAsync("/api/games", request);

            Assert.Equal(HttpStatusCode.Created, respone.StatusCode);

            var createdGame = await respone.Content.ReadFromJsonAsync<GameResponse>();

            Assert.NotNull(createdGame);
            Assert.True(createdGame.Id > 0);
            Assert.Equal(request.Title, createdGame.Title);
            Assert.Equal(request.Genre, createdGame.Genre);
            Assert.Equal(request.ReleaseYear, createdGame.ReleaseYear);

            Assert.NotNull(respone.Headers.Location);

            var getResponse = await _httpClient.GetAsync(respone.Headers.Location);

            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        }
        
        [Fact]
        public async Task CreateGame_WithInvalidRequest_ReturnsBadRequest()
        {
            var request = new CreateGameRequest
            {
                Title = "I should not work",
                Genre = "No Hail Marys this time",
                ReleaseYear = 0
            };

            var response = await _httpClient.PostAsJsonAsync("/api/games", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateGame_WhenGameDoesNotExist_ReturnsNotFound()
        {
            var request = new UpdateGameRequest
            {
                Title = "Missing game",
                Genre = "Unknown",
                ReleaseYear = 2025
            };

            var response = await _httpClient.PutAsJsonAsync($"/api/games/{999999}", request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdateGame_WhenGameExists_ReturnsNoContentAndUpdatesGame()
        {
            var createRequest = new CreateGameRequest
            {
                Title = "Original title",
                Genre = "Original genre",
                ReleaseYear = 2020
            };

            var createResponse = await _httpClient.PostAsJsonAsync("/api/games", createRequest);

            Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

            var createdGame = await createResponse.Content.ReadFromJsonAsync<GameResponse>();

            Assert.NotNull(createdGame);

            var updateRequest = new UpdateGameRequest
            {
                Title = "Updated title",
                Genre = "Updated genre",
                ReleaseYear = 2025
            };

            var updateResponse = await _httpClient.PutAsJsonAsync($"/api/games/{createdGame.Id}", updateRequest);

            Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);

            var getResponse = await _httpClient.GetAsync($"/api/games/{createdGame.Id}");

            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

            var updatedGame = await getResponse.Content.ReadFromJsonAsync<GameResponse>();

            Assert.NotNull(updatedGame);
            Assert.Equal(updateRequest.Title, updatedGame.Title);
            Assert.Equal(updateRequest.Genre, updatedGame.Genre);
            Assert.Equal(updateRequest.ReleaseYear, updatedGame.ReleaseYear);
        }

        [Fact]
        public async Task DeleteGame_WhenGameExists_ReturnsNoContent()
        {
            // Arrange: create a game specifically for deletion.
            var createRequest = new CreateGameRequest
            {
                Title = "Delete me",
                Genre = "Temporary",
                ReleaseYear = 2024
            };

            var createResponse = await _httpClient.PostAsJsonAsync("/api/games", createRequest);

            var createdGame = await createResponse.Content.ReadFromJsonAsync<GameResponse>();

            Assert.NotNull(createdGame);

            // Act
            var deleteResponse = await _httpClient.DeleteAsync($"/api/games/{createdGame.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

            // Prove it no longer exists.
            var getResponse = await _httpClient.GetAsync($"/api/games/{createdGame.Id}");

            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }

        [Fact]
        public async Task DeleteGame_WhenGameDoesNotExist_ReturnsNotFound()
        {
            var response = await _httpClient.DeleteAsync($"/api/games/{999999}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


    }
}
