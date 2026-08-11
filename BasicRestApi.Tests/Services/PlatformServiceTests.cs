using BasicRestApi.Data;
using BasicRestApi.Models;
using BasicRestApi.Services;
using Microsoft.EntityFrameworkCore;

namespace BasicRestApi.Tests.Services
{
    public class PlatformServiceTests
    {
        private readonly IPlatformService _platformService;

        private readonly GameDbContext _GameDbContext;

        public PlatformServiceTests()
        {
            var options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _GameDbContext = new GameDbContext(options);

            _GameDbContext.Platforms.AddRange(
                new Platform
                {
                    Id = 1,
                    Name = "Playstation 5",
                    Manufacturer = "Sony",
                    ReleaseYear = 1999
                },
                new Platform
                {
                    Id = 2,
                    Name = "XBox",
                    Manufacturer = "Microsoft",
                    ReleaseYear = 1999
                },
                new Platform
                {
                    Id = 3,
                    Name = "Steam Machine",
                    Manufacturer = "Valve",
                    ReleaseYear = 1999
                });

            _GameDbContext.SaveChanges();

            _platformService = new PlatformService(_GameDbContext);
        }


        [Theory]
        [InlineData(1, "Playstation 5", "Sony", 1999)]
        [InlineData(2, "XBox", "Microsoft", 1999)]
        [InlineData(3, "Steam Machine", "Valve", 1999)]
        public void GetAllPlatforms_WhenCalled_ShouldReturnAllSeededPlatforms(
            int expectedId,
            string expectedName,
            string expectedManufacturer,
            int expectedReleaseYear)
        {
            // Act
            var retrievedPlatforms = _platformService.GetPlatforms();

            // Assert
            var platform = Assert.Single(
                retrievedPlatforms,
                p => p.Id == expectedId);

            Assert.Equal(3, retrievedPlatforms.Count);
            Assert.Equal(expectedName, platform.Name);
            Assert.Equal(expectedManufacturer, platform.Manufacturer);
            Assert.Equal(expectedReleaseYear, platform.ReleaseYear);
        }

        [Fact]
        public void GetPlatform_ById_WhenPlatformExists_ReturnMatchingPlatform()
        {
            // Arrange
            const int platformId = 1;

            // Act
            var result = _platformService.GetPlatormById(platformId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(platformId, result.Id);
        }

        [Fact]
        public void GetPlatformId_WhenPlatformDoesNotExist_ReturnsNull()
        {
            // Arrange
            const int missingPlatformId = 999;

            // Act
            var result = _platformService.GetPlatormById(missingPlatformId);

            Assert.Null(result);
        }

        [Fact]
        public void CreatePlatform_ReturnMatchPlatform()
        {
            // Arrange
            var newPlatform = new Platform
            {
                Name = "Metal Gear Solid",
                Manufacturer = "Tactical Espionage",
                ReleaseYear = 1993
            };

            // Act
            var result = _platformService.CreatePlatform(newPlatform);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Id > 0);
            Assert.Equal(newPlatform.Name, result.Name);
            Assert.Equal(newPlatform.Manufacturer, result.Manufacturer);
            Assert.Equal(newPlatform.ReleaseYear, result.ReleaseYear);
        }

        [Fact]
        public void CreatePlatform_WhenCalled_ShouldBeStoresSuccessfully()
        {
            // Arrange
            var newPlatform = new Platform
            {
                Name = "Metal Gear Solid",
                Manufacturer = "Tactical Espionage",
                ReleaseYear = 1993
            };

            // Act
            var result = _platformService.CreatePlatform(newPlatform);

            var retrievedPlatform = _platformService.GetPlatormById(result.Id);

            // Assert
            Assert.NotNull(retrievedPlatform);
            Assert.Equal(result.Id, retrievedPlatform.Id);
            Assert.Equal(newPlatform.Name, retrievedPlatform.Name);
            Assert.Equal(newPlatform.Manufacturer, retrievedPlatform.Manufacturer);
            Assert.Equal(newPlatform.ReleaseYear, retrievedPlatform.ReleaseYear);
        }

        [Fact]
        public void UpdatePlatform_WhenCalled_ExistingIdShouldReturnTrue()
        {
            // Arrange
            var updatePlatform = new Platform
            {
                Id = 1,
                Name = "I am a test",
                Manufacturer = "Seagate",
                ReleaseYear = 2026,
            };

            // Act
            var actual = _platformService.UpdatePlatform(updatePlatform.Id, updatePlatform);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void UpdatePlatform_WhenCalled_NonExistingIdShouldReturnFalse()
        {
            // Arrange
            var updatedPlatform = new Platform
            {
                Name = "Test",
                Manufacturer = "aa",
                ReleaseYear = 1999

            };

            // Act

            var actual = _platformService.UpdatePlatform(updatedPlatform.Id, updatedPlatform);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void UpdatePlatform_WhenCalled_ShouldReturnUpdatedProperties()
        {
            // Arrange
            var newPlatform = new Platform
            {
                Name = "Test",
                Manufacturer = "aa",
                ReleaseYear = 1999

            };

            var platformAdded = _platformService.CreatePlatform(newPlatform);

            var updatedPlatform = new Platform
            {
                Name = "Test Twp",
                Manufacturer = "bb",
                ReleaseYear = 1988

            };

            // Act
            var isSuccessful = _platformService.UpdatePlatform(platformAdded.Id, updatedPlatform);
            var platformFound = _platformService.GetPlatormById(platformAdded.Id);

            // Assert
            Assert.True(isSuccessful);
            Assert.NotNull(platformFound);
            Assert.Equal(platformAdded.Id, platformFound.Id);
            Assert.Equal(updatedPlatform.Name, platformFound.Name);
            Assert.Equal(updatedPlatform.Manufacturer, platformFound.Manufacturer);
            Assert.Equal(updatedPlatform.ReleaseYear, platformFound.ReleaseYear);
        }

        [Fact]
        public void DeletePlatform_WhenCalled_ShouldDeleteSucessfully()
        {
            // Arrange
            var newPlatform = new Platform
            {
                Name = "Metal Gear Solid",
                Manufacturer = "Tactical Espionage",
                ReleaseYear = 1993
            };


            var PlatformCreated = _platformService.CreatePlatform(newPlatform);

            // Act
            var PlatformDeleted = _platformService.DeletePlatform(PlatformCreated.Id);
            var retrievePlatform = _platformService.GetPlatormById(PlatformCreated.Id);

            // Assert
            Assert.True(PlatformDeleted);
            Assert.Null(retrievePlatform);
        }

        [Fact]
        public void DeletePlatform_WhenCalled_ShouldReturnFalse_WhenPlatformNotFound()
        {
            // Arrange
            int nonExistentId = int.MaxValue;

            // Act 
            var actual = _platformService.DeletePlatform(nonExistentId);

            // Assert
            Assert.False(actual);
        }


    }
}
