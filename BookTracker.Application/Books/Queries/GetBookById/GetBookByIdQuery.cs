using BookTracker.Domain.Entities;
using MediatR;

namespace BookTracker.Application.Books.Queries.GetBookById;

// Returns null if the book doesn't exist.
public record GetBookByIdQuery(int Id) : IRequest<Book?>;
