using BookTracker.Domain.Entities;
using MediatR;

namespace BookTracker.Application.Books.Queries.GetBooks;

public record GetBooksQuery : IRequest<IEnumerable<Book>>;
