using BookTracker.Domain.Entities;

namespace BookTracker.Application.Interfaces;

public interface IBookRepository
{
    IEnumerable<Book> GetAll();
    Book? GetById(int id);
    Book Add(Book book);
    bool Update(Book book);
    bool Delete(int id);
}
