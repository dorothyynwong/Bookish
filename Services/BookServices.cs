using Bookish.Models;
using Bookish.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bookish.Services
{
    public class BookService(BookishContext context)
    {
        private readonly BookishContext _context = context;

        public async Task AddBookAuthor(BookAuthorModel bookAuthor)
        {
            int authorId = await GetAuthorIdByName(bookAuthor.AuthorFirstName, bookAuthor.AuthorSurname);
            
            if (authorId == 0)
            {
                Author author = new Author
                        {
                            FirstName = bookAuthor.AuthorFirstName,
                            Surname = bookAuthor.AuthorSurname
                        }; 
                authorId = await AddAuthor(author);    
            } 
            
            Book book = new Book 
                        {
                            ISBN = bookAuthor.ISBN,
                            BookName = bookAuthor.BookName,
                            AuthorId = authorId,
                            NumberOfCopies = bookAuthor.NumberOfCopies,
                            AvailableCopies = bookAuthor.AvailableCopies,
                            Genre = bookAuthor.Genre
                        };

            await AddBook(book);


            // await AddAuthor(author);
        }

        public async Task AddBook(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task<int> AddAuthor(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return await GetAuthorIdByName(author.FirstName, author.Surname);
        }

        public async Task<int> GetAuthorIdByName(string firstName, string surname)
        {
            Author? author = await _context.Authors.FirstOrDefaultAsync(author => author.FirstName == firstName && author.Surname == surname);
            return author!=null ? author.Id : 0;
        }
    }

    
}   
