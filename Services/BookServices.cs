using Bookish.Models;
using Bookish.ViewModels;

namespace Bookish.Services
{
    public class BookService(BookishContext context)
    {
        private readonly BookishContext _context = context;

        public async Task AddBookAuthor(BookAuthorModel bookAuthor)
        {
            Book book = new Book 
                        {
                            ISBN = bookAuthor.ISBN,
                            BookName = bookAuthor.BookName,
                            NumberOfCopies = bookAuthor.NumberOfCopies,
                            AvailableCopies = bookAuthor.AvailableCopies,
                            Genre = bookAuthor.Genre
                        };
            await AddBook(book);

            Author author = new Author
                        {
                            FirstName = bookAuthor.AuthorFirstName,
                            Surname = bookAuthor.AuthorSurname
                        };
            // await AddAuthor(author);
        }

        public async Task AddBook(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task AddAuthor(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }
    }

    
}   
