using BookTracker.Application.Interfaces;
using MediatR;

namespace BookTracker.Application.Books.Commands.DeleteBook;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
{
    private readonly IBookRepository _repository;

    public DeleteBookCommandHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_repository.Delete(request.Id));
    }
}
