using BookTracker.Domain.Entities;
using MediatR;

namespace BookTracker.Application.Books.Commands.CreateBook;

public record CreateBookCommand(string Title, string Author) : IRequest<Book>;
