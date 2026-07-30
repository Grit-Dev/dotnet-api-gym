using BasicRestApi.Data;
using BasicRestApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

// Scaffolding built to make Integration easier to test
namespace BasicRestApi.Tests.Integration
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        // One test database shared by this factory.
        private readonly string _databaseName = $"GamesIntegrationTests-{Guid.NewGuid()}";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the real SQLite configuration.
                var dbContextDescriptor =
                    services.SingleOrDefault(service =>
                        service.ServiceType ==
                        typeof(IDbContextOptionsConfiguration<GameDbContext>));

                if (dbContextDescriptor is not null)
                {
                    services.Remove(dbContextDescriptor);
                }

                // Replace SQLite with a test-only in-memory database.
                services.AddDbContext<GameDbContext>(options =>
                    options.UseInMemoryDatabase(_databaseName));

                var serviceProvider =
                    services.BuildServiceProvider();

                using var scope =
                    serviceProvider.CreateScope();

                var context =
                    scope.ServiceProvider
                        .GetRequiredService<GameDbContext>();

                context.Database.EnsureCreated();

                if (!context.Games.Any())
                {
                    context.Games.AddRange(
                        new Game
                        {
                            Title = "The Witcher 3",
                            Genre = "Action RPG",
                            ReleaseYear = 2015
                        },
                        new Game
                        {
                            Title = "Cyberpunk 2077",
                            Genre = "Action RPG",
                            ReleaseYear = 2020
                        });

                    context.SaveChanges();
                }
            });
        }
    }
}