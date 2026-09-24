using BookTracker.Application.Interfaces;
using BookTracker.Domain.Entities;
using MediatR;

namespace BookTracker.Application.Books.Queries.GetBookById;

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book?>
{
    private readonly IBookRepository _repository;

    public GetBookByIdQueryHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public Task<Book?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_repository.GetById(request.Id));
    }
}
