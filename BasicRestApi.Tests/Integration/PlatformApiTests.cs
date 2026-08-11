using BasicRestApi.Dtos;
using System.Net;
using System.Net.Http.Json;

namespace BasicRestApi.Tests.Integration
{
    // Starts the real api in memory WAF
    public class PlatformApiTests : IClassFixture<CustomWebApplicationFactory>
    {
        // Sends a real HTTP Request
        private readonly HttpClient _httpClient;

        public PlatformApiTests(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task GetPlatform_ReturnsOk()
        {
            var response = await _httpClient.GetAsync("/api/Platforms");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetPlatform_ReturnsOkWithPlatform()
        {
            var response = await _httpClient.GetAsync("/api/Platforms");


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var Platform = await response.Content.ReadFromJsonAsync<List<PlatformResponse>>();

            Assert.NotNull(Platform);
            Assert.NotEmpty(Platform);
        }

        [Fact]
        public async Task GetPlatform_ReturnsNotFound_WhenPlatformDoesNotExist()
        {
            var response = await _httpClient.GetAsync("/api/Platforms/999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetPlatformById_WhenPlatformExists_ReturnsOkWithPlatform()
        {
            var allPlatformFromResponse = await _httpClient.GetAsync("/api/Platforms");

            var Platform = await allPlatformFromResponse.Content.ReadFromJsonAsync<List<PlatformResponse>>();

            Assert.NotNull(Platform);
            Assert.NotEmpty(Platform);

            var expectedPlatform = Platform[0];

            var response = await _httpClient.GetAsync($"/api/Platforms/{expectedPlatform.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var actualPlatform = await response.Content.ReadFromJsonAsync<PlatformResponse>();

            Assert.NotNull(actualPlatform);
            Assert.Equal(expectedPlatform.Id, actualPlatform.Id);
            Assert.Equal(expectedPlatform.Name, actualPlatform.Name);
            Assert.Equal(expectedPlatform.Name, actualPlatform.Name);
            Assert.Equal(expectedPlatform.ReleaseYear, actualPlatform.ReleaseYear);
        }

        [Fact]
        public async Task CreatePlatform_WithValidRequest_ReturnsCreated()
        {
            var request = new CreatePlatformRequest
            {
                Name = "I hope you work",
                Manufacturer = "Hail Mary",
                ReleaseYear = 2026
            };

            var respone = await _httpClient.PostAsJsonAsync("/api/Platforms", request);

            Assert.Equal(HttpStatusCode.Created, respone.StatusCode);

            var createdPlatform = await respone.Content.ReadFromJsonAsync<PlatformResponse>();

            Assert.NotNull(createdPlatform);
            Assert.True(createdPlatform.Id > 0);
            Assert.Equal(request.Name, createdPlatform.Name);
            Assert.Equal(request.Manufacturer, createdPlatform.Manufacturer);
            Assert.Equal(request.ReleaseYear, createdPlatform.ReleaseYear);

            Assert.NotNull(respone.Headers.Location);

            var getResponse = await _httpClient.GetAsync(respone.Headers.Location);

            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        }
        
        [Fact]
        public async Task CreatePlatform_WithInvalidRequest_ReturnsBadRequest()
        {
            var request = new CreatePlatformRequest
            {
                Name = "  ",
                Manufacturer = "Hail Mary",
                ReleaseYear = 2026
            };

            var response = await _httpClient.PostAsJsonAsync("/api/Platforms", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdatePlatform_WhenPlatformDoesNotExist_ReturnsNotFound()
        {
            var request = new UpdatePlatformRequest
            {
                Name = "I hope you work",
                Manufacturer = "Hail Mary",
                ReleaseYear = 2026
            };

            var response = await _httpClient.PutAsJsonAsync($"/api/Platforms/{999999}", request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdatePlatform_WhenPlatformExists_ReturnsNoContentAndUpdatesPlatform()
        {
            var createRequest = new UpdatePlatformRequest
            {
                Name = "I hope you work",
                Manufacturer = "Hail Mary",
                ReleaseYear = 2026
            };

            var createResponse = await _httpClient.PostAsJsonAsync("/api/Platforms", createRequest);

            Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

            var createdPlatform = await createResponse.Content.ReadFromJsonAsync<PlatformResponse>();

            Assert.NotNull(createdPlatform);

            var updateRequest = new UpdatePlatformRequest
            {
                Name = "Updated title",
                Manufacturer = "Updated genre",
                ReleaseYear = 2025
            };

            var updateResponse = await _httpClient.PutAsJsonAsync($"/api/Platforms/{createdPlatform.Id}", updateRequest);

            Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);

            var getResponse = await _httpClient.GetAsync($"/api/Platforms/{createdPlatform.Id}");

            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

            var updatedPlatform = await getResponse.Content.ReadFromJsonAsync<PlatformResponse>();

            Assert.NotNull(updatedPlatform);
            Assert.Equal(updateRequest.Name, updatedPlatform.Name);
            Assert.Equal(updateRequest.Manufacturer, updatedPlatform.Manufacturer);
            Assert.Equal(updateRequest.ReleaseYear, updatedPlatform.ReleaseYear);
        }

        [Fact]
        public async Task DeletePlatform_WhenPlatformExists_ReturnsNoContent()
        {
            // Arrange: create a Platform specifically for deletion.
            var createRequest = new UpdatePlatformRequest
            {
                Name = "I hope you work",
                Manufacturer = "Hail Mary",
                ReleaseYear = 2026
            };

            var createResponse = await _httpClient.PostAsJsonAsync("/api/Platforms", createRequest);

            var createdPlatform = await createResponse.Content.ReadFromJsonAsync<PlatformResponse>();

            Assert.NotNull(createdPlatform);

            // Act
            var deleteResponse = await _httpClient.DeleteAsync($"/api/Platforms/{createdPlatform.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

            // Prove it no longer exists.
            var getResponse = await _httpClient.GetAsync($"/api/Platforms/{createdPlatform.Id}");

            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }

        [Fact]
        public async Task DeletePlatform_WhenPlatformDoesNotExist_ReturnsNotFound()
        {
            var response = await _httpClient.DeleteAsync($"/api/Platforms/{999999}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


    }
}
