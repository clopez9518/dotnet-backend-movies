

using Movies.Infrastructure.Extensions;
using Movies.Infrastructure.Persistence;
using Movies.Infrastructure.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure.Middleware;
using Movies.Application.Extensions;
using Movies.Api.Filters;
using Movies.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Config cors
var frontendUrl = builder.Configuration["CorsSettings:FrontendUrl"];

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        if (!string.IsNullOrEmpty(frontendUrl))
        {
            policy.WithOrigins(frontendUrl)
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        }
    });
});

// Add services to the container.
builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add<ValidationFilter>();
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Presentation Dependency Injection
builder.Services.AddPresentation();
//Application Dependency Injection
builder.Services.AddApplication();
//Infrastructure Dependency Injection 
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDBContext>();
        var seeder = services.GetRequiredService<DbInitializer>();

        await context.Database.MigrateAsync();
        await seeder.SeedAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during migration/seed: {ex.Message}");
        throw;
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("AllowFrontend");

if (!app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
