using Bookish.Models;
using Bookish.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookish.Interfaces
{
    public interface IBookService
    {
        Task AddBookAuthor(BookAuthorModel bookAuthor);
        Task AddBook(Book book);
        Task<int> AddAuthor(Author author);
        Task<int> GetAuthorIdByName(string firstName, string surname);
        Task<Book> UpdateBookCopy(int id, bool checkOut);
        Task UpdateBook(BookAuthorModel bookAuthor);
        Task<BookAuthorModel?> GetBookAuthorById(string id);
        Task<List<BookAuthorModel>?> FilterBooks(string? filterType, string? filterValue);
        Task<Book?> GetBookByBookId(int id);
    }
}
