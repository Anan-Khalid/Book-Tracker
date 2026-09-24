using BookTracker.Application.Interfaces;
using BookTracker.Domain.Entities;
using MediatR;

namespace BookTracker.Application.Books.Commands.CreateBook;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Book>
{
    private readonly IBookRepository _repository;

    public CreateBookCommandHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book { Title = request.Title, Author = request.Author };
        return Task.FromResult(_repository.Add(book));
    }
}
