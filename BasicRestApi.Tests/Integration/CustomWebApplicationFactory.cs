using BasicRestApi.Data;
using BasicRestApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BasicRestApi.Tests.Integration
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        // One isolated test database per factory instance.
        private readonly string _databaseName =
            $"BasicRestApiIntegrationTests-{Guid.NewGuid()}";


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove production SQLite configuration.
                var dbContextDescriptor =
                    services.SingleOrDefault(service =>
                        service.ServiceType ==
                        typeof(IDbContextOptionsConfiguration<GameDbContext>));

                if (dbContextDescriptor is not null)
                {
                    services.Remove(dbContextDescriptor);
                }


                // Add test-only InMemory database.
                services.AddDbContext<GameDbContext>(options =>
                    options.UseInMemoryDatabase(_databaseName));


                // Create service provider after replacing DbContext.
                var provider = services.BuildServiceProvider();

                using var scope = provider.CreateScope();

                var context = scope.ServiceProvider
                    .GetRequiredService<GameDbContext>();


                context.Database.EnsureCreated();


                SeedDatabase(context);
            });
        }


        private static void SeedDatabase(GameDbContext context)
        {
            Developer cdProjekt;

            // Seed developer
            if (!context.Developers.Any())
            {
                cdProjekt = new Developer
                {
                    Name = "CD PROJEKT"
                };

                context.Developers.Add(cdProjekt);
                context.SaveChanges();
            }
            else
            {
                cdProjekt = context.Developers.First();
            }


            // Seed platforms
            if (!context.Platforms.Any())
            {
                context.Platforms.AddRange(
                    new Platform
                    {
                        Name = "PlayStation 5",
                        Manufacturer = "Sony",
                        ReleaseYear = 2020
                    },
                    new Platform
                    {
                        Name = "Xbox Series X",
                        Manufacturer = "Microsoft",
                        ReleaseYear = 2020
                    });

                context.SaveChanges();
            }


            var platforms = context.Platforms.ToList();


            // Seed games
            if (!context.Games.Any())
            {
                context.Games.AddRange(
                    new Game
                    {
                        Title = "The Witcher 3",
                        Genre = "Action RPG",
                        ReleaseYear = 2015,
                        DeveloperId = cdProjekt.Id,
                        Platforms = platforms
                    },
                    new Game
                    {
                        Title = "Cyberpunk 2077",
                        Genre = "Action RPG",
                        ReleaseYear = 2020,
                        DeveloperId = cdProjekt.Id,
                        Platforms = platforms
                    });

                context.SaveChanges();
            }
        }
    }
}