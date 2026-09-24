using MediatR;

namespace BookTracker.Application.Books.Commands.DeleteBook;

// Returns true if the book was deleted, false if it doesn't exist.
public record DeleteBookCommand(int Id) : IRequest<bool>;
