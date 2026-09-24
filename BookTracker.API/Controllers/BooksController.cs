using BookTracker.Application.Books.Commands.CreateBook;
using BookTracker.Application.Books.Commands.DeleteBook;
using BookTracker.Application.Books.Commands.UpdateBook;
using BookTracker.Application.Books.Queries.GetBookById;
using BookTracker.Application.Books.Queries.GetBooks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookTracker.API.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _mediator.Send(new GetBooksQuery());
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _mediator.Send(new GetBookByIdQuery(id));
        return book is null ? NotFound() : Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookCommand command)
    {
        var book = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateBookRequest request)
    {
        var updated = await _mediator.Send(new UpdateBookCommand(id, request.Title, request.Author));
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _mediator.Send(new DeleteBookCommand(id));
        return deleted ? NoContent() : NotFound();
    }
}

// The id comes from the URL, so the request body only needs these two fields.
public record UpdateBookRequest(string Title, string Author);
