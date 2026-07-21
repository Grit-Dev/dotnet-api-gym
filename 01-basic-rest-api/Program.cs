using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BasicRestApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Register controller support.
        builder.Services.AddControllers();

        // Register OpenAPI document generation.
        builder.Services.AddOpenApi();

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