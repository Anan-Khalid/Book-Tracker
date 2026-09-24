using BookTracker.Application.Interfaces;
using BookTracker.Domain.Entities;
using MediatR;

namespace BookTracker.Application.Books.Commands.UpdateBook;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, bool>
{
    private readonly IBookRepository _repository;

    public UpdateBookCommandHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book { Id = request.Id, Title = request.Title, Author = request.Author };
        return Task.FromResult(_repository.Update(book));
    }
}
