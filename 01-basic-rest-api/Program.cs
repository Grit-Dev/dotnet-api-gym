using BasicRestApi.Data;
using BasicRestApi.Services;
using Microsoft.EntityFrameworkCore;

namespace BasicRestApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register controller support.
            builder.Services.AddControllers();

            // Register OpenAPI document generation.
            builder.Services.AddOpenApi();


            var databasePath =
            Path.Combine(builder.Environment.ContentRootPath, "games.db");
            builder.Services.AddDbContext<GameDbContext>(options =>
                options.UseSqlite($"Data Source={databasePath}"));

            builder.Services.AddScoped<IGameService, GameService>();

            var app = builder.Build();

            // Enable API documentation during development.
            if (app.Environment.IsDevelopment())
            {
                // Generates the OpenAPI document.
                app.MapOpenApi();

                // Displays the document through Swagger UI.
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint(
                        "/openapi/v1.json",
                        "Basic REST API v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Finds and activates routes from every controller.
            app.MapControllers();

            app.Run();
        }
    }
}