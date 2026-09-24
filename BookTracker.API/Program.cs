using BookTracker.Application.Books.Commands.CreateBook;
using BookTracker.Application.Interfaces;
using BookTracker.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MediatR: scan the Application assembly and register every handler it finds.
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateBookCommand).Assembly));

// Dependency Injection: whenever someone asks for IBookRepository,
// give them the SQLite implementation. Singleton = one instance for the app's lifetime.
// The database file is created next to where you run the app.
var connectionString = "Data Source=booktracker.db";
builder.Services.AddSingleton<IBookRepository>(_ => new SqliteBookRepository(connectionString));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
