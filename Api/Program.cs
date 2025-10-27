using BookCatalog.Application.Repositories;
using BookCatalog.Application.Services.Implementatios;
using BookCatalog.Application.Services.Interfaces;
using BookCatalog.Infrastructure.Data;
using BookCatalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5065", "https://localhost:7146");

builder.Services.AddControllers();

// InMemory Db
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("BookCatalogDB"));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI Registrations
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

// Create a scoped service provider for one-time setup tasks
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Retrieve the ApplicationDbContext from DI
        var context = services.GetRequiredService<ApplicationDbContext>();

        // Ensure the in-memory database is created
        context.Database.EnsureCreated();

        // Seed initial data into the database (authors, books, etc.)
        SeedData.Initialize(context);
    }
    catch (Exception ex)
    {
        // Log any errors that occur during seeding
        var logger = services.GetRequiredService<ILogger<Program>>();

        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
