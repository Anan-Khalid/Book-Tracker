using MediatR;

namespace BookTracker.Application.Books.Commands.UpdateBook;

// Returns true if the book was found and updated, false if it doesn't exist.
public record UpdateBookCommand(int Id, string Title, string Author) : IRequest<bool>;
