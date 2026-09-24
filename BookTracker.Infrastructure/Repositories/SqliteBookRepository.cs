using BookTracker.Application.Interfaces;
using BookTracker.Domain.Entities;
using Microsoft.Data.Sqlite;

namespace BookTracker.Infrastructure.Repositories;

public class SqliteBookRepository : IBookRepository
{
    private readonly string _connectionString;

    public SqliteBookRepository(string connectionString)
    {
        _connectionString = connectionString;

        // Create the table on startup if it doesn't exist yet.
        using var connection = Open();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Books (
                Id     INTEGER PRIMARY KEY AUTOINCREMENT,
                Title  TEXT NOT NULL,
                Author TEXT NOT NULL
            );";
        command.ExecuteNonQuery();
    }

    public IEnumerable<Book> GetAll()
    {
        using var connection = Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Title, Author FROM Books";

        var books = new List<Book>();
        using var reader = command.ExecuteReader();
        while (reader.Read())
            books.Add(ReadBook(reader));
        return books;
    }

    public Book? GetById(int id)
    {
        using var connection = Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Title, Author FROM Books WHERE Id = $id";
        command.Parameters.AddWithValue("$id", id);

        using var reader = command.ExecuteReader();
        return reader.Read() ? ReadBook(reader) : null;
    }

    public Book Add(Book book)
    {
        using var connection = Open();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Books (Title, Author) VALUES ($title, $author);
            SELECT last_insert_rowid();";
        command.Parameters.AddWithValue("$title", book.Title);
        command.Parameters.AddWithValue("$author", book.Author);

        book.Id = (int)(long)command.ExecuteScalar()!;
        return book;
    }

    public bool Update(Book book)
    {
        using var connection = Open();
        using var command = connection.CreateCommand();
        command.CommandText = "UPDATE Books SET Title = $title, Author = $author WHERE Id = $id";
        command.Parameters.AddWithValue("$id", book.Id);
        command.Parameters.AddWithValue("$title", book.Title);
        command.Parameters.AddWithValue("$author", book.Author);

        return command.ExecuteNonQuery() > 0;
    }

    public bool Delete(int id)
    {
        using var connection = Open();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Books WHERE Id = $id";
        command.Parameters.AddWithValue("$id", id);

        return command.ExecuteNonQuery() > 0;
    }

    private SqliteConnection Open()
    {
        var connection = new SqliteConnection(_connectionString);
        connection.Open();
        return connection;
    }

    private static Book ReadBook(SqliteDataReader reader) => new()
    {
        Id = reader.GetInt32(0),
        Title = reader.GetString(1),
        Author = reader.GetString(2)
    };
}
