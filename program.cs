var builder = WebApplication.CreateBuilder(args);

// Register controller support.
builder.Services.AddControllers();

// Generate the OpenAPI description.
builder.Services.AddOpenApi();

var app = builder.Build();

// Enable OpenAPI during local development.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();