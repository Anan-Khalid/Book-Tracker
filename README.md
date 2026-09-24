# BookTracker

## The idea
BookTracker is a tiny app for keeping a list of books. You can add a book (title and author), see all your books, look one up, change it, or delete it. Everything is saved in a small file on your computer, so your list is still there next time.

It was built as a learning project to show how a well-organized backend is put together, without making it complicated.

## Technologies
- **C# / .NET 8** with **ASP.NET Core Web API**
- **MediatR**: sends each request to the right piece of code
- **SQLite**: a simple database stored in one file (`booktracker.db`)
- **Swagger**: a web page for trying out the API without writing any code

## Structure
The project is split into four parts, each with one job:

| Project | What it does |
|---|---|
| **Domain** | Defines what a Book is (Id, Title, Author) |
| **Application** | The actions the app can do (create, update, delete, list, get one) and the rules for saving books |
| **Infrastructure** | Actually saves and loads books from SQLite |
| **API** | Receives web requests and sends back answers |

A request flows like this:

```text
API Controller -> MediatR -> Command/Query Handler -> Repository -> SQLite
```

Actions that change data (**Commands**) are kept separate from actions that only read data (**Queries**). This idea is called CQRS.

## How to run
1. Install the [.NET 8 SDK](https://dotnet.microsoft.com/download).
2. Open a terminal in the folder that contains `BookTracker.sln`.
3. Run:
   ```
   dotnet run --project BookTracker.API --urls http://localhost:5000
   ```
4. Open **http://localhost:5000/swagger** in your browser and use "Try it out" on any endpoint.

Example to add a book:
```json
{ "title": "Clean Code", "author": "Robert C. Martin" }
```

To reset the data, stop the app and delete `booktracker.db`.

## Future improvements
- Check input (for example, no empty titles)
- Friendlier error messages
- More book details (year, genre, read/unread status)
- Search, sorting and paging for long lists
- Async database calls
- Automated tests
- User accounts and login
- A simple front-end web page
