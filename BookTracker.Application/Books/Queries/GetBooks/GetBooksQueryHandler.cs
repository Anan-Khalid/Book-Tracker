using BookTracker.Application.Interfaces;
using BookTracker.Domain.Entities;
using MediatR;

namespace BookTracker.Application.Books.Queries.GetBooks;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<Book>>
{
    private readonly IBookRepository _repository;

    public GetBooksQueryHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_repository.GetAll());
    }
}
