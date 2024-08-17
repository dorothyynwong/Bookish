using Bookish.Models;
using Bookish.ViewModels;
using Microsoft.EntityFrameworkCore;
using Bookish.Interfaces;
using Bookish;

namespace Bookish.Services
{
    public class BookService: IBookService
    {
        private readonly BookishContext _context;
        public BookService(BookishContext context)
        {
            _context = context;
        }

        public async Task AddBookAuthor(BookAuthorModel bookAuthor)
        {
            int authorId = await GetAuthorIdByName(bookAuthor.AuthorFirstName, bookAuthor.AuthorSurname);

            if (authorId == 0)
            {
                Author author = new()
                {
                    FirstName = bookAuthor.AuthorFirstName,
                    Surname = bookAuthor.AuthorSurname
                };
                authorId = await AddAuthor(author);
            }

            Book book = new()
            {
                ISBN = bookAuthor.ISBN,
                BookName = bookAuthor.BookName,
                AuthorId = authorId,
                NumberOfCopies = bookAuthor.NumberOfCopies,
                AvailableCopies = bookAuthor.AvailableCopies,
                Genre = bookAuthor.Genre
            };

            await AddBook(book);
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
            Author? author = await _context.Authors.FirstOrDefaultAsync(author =>
                            author.FirstName == firstName && author.Surname == surname);
            return author != null ? author.Id : 0;
        }

        public async Task<Book> UpdateBookCopy(int id, bool checkOut)
        {

            Book? book = await GetBookByBookId(id);
            if (checkOut)
            {
                if (book.AvailableCopies > 0)
                {
                    book.AvailableCopies--;
                }
            }
            else
            {
                if (book.AvailableCopies < book.NumberOfCopies)
                {
                    book.AvailableCopies++;
                }
            }

            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return book;

        }

        public async Task UpdateBook(BookAuthorModel bookAuthor)
        {
            int authorId = await GetAuthorIdByName(bookAuthor.AuthorFirstName, bookAuthor.AuthorSurname);

            if (authorId == 0)
            {
                Author author = new()
                {
                    FirstName = bookAuthor.AuthorFirstName,
                    Surname = bookAuthor.AuthorSurname
                };
                authorId = await AddAuthor(author);
            }

            Book book = new()
            {
                Id = bookAuthor.Id,
                ISBN = bookAuthor.ISBN,
                BookName = bookAuthor.BookName,
                AuthorId = authorId,
                NumberOfCopies = bookAuthor.NumberOfCopies,
                AvailableCopies = bookAuthor.AvailableCopies,
                Genre = bookAuthor.Genre
            };

            _context.Update(book);

            await _context.SaveChangesAsync();
        }

        public async Task<BookAuthorModel?> GetBookAuthorById(string id)
        {
            if (int.TryParse(id, out int idNo))
            {
                Book? book = await _context.Books.FindAsync(idNo);
                Author? author = await _context.Authors.FindAsync(book.AuthorId);
                if (author != null && book != null)
                {
                    BookAuthorModel? bookAuthor = new()
                    {
                        Id = book.Id,
                        ISBN = book.ISBN,
                        BookName = book.BookName,
                        AuthorId = book.AuthorId,
                        AuthorFirstName = author.FirstName,
                        AuthorSurname = author.Surname,
                        NumberOfCopies = book.NumberOfCopies,
                        AvailableCopies = book.AvailableCopies,
                        Genre = book.Genre
                    };
                    return bookAuthor;

                }
                else return null;

            }
            else return null;
        }

        public async Task<List<BookAuthorModel>?> FilterBooks(string? filterType, string? filterValue)
        {
            List<BookAuthorModel> bookAuthorList = [];
            IQueryable<Book> query = _context.Books; // SELECT * FROM Books;  
            switch (filterType)
            {
                case "genre":
                    query = query.Where(book => book.Genre.ToLower() == filterValue); // WHERE Genre = "Fantasy"
                    break;
                case "bookname":
                    query = query.Where(book => book.BookName.ToLower().Contains(filterValue != null ? filterValue : "")); // WHERE Genre = "Fantasy"
                    break;
                case "author": //LINQ Methods
                    var authorQuery = _context.Books.Join(_context.Authors,
                        book => book.AuthorId,
                        author => author.Id,
                        (book, author) => new BookAuthorModel
                        {
                            Id = book.Id,
                            ISBN = book.ISBN,
                            BookName = book.BookName,
                            AuthorId = book.AuthorId,
                            AuthorFirstName = author.FirstName,
                            AuthorSurname = author.Surname,
                            NumberOfCopies = book.NumberOfCopies,
                            AvailableCopies = book.AvailableCopies,
                            Genre = book.Genre
                        })
                        .Where(book => (book.AuthorFirstName.ToLower() + " " + book.AuthorSurname.ToLower()).Contains(filterValue != null ? filterValue : ""))
                        .OrderBy(book => book.BookName);
                    return await authorQuery.ToListAsync();
                // case "authortest": //LINQ Query
                // 	var bookAuthors = await (from book in _context.Books
                // 	   join author in _context.Authors on book.AuthorId equals author.Id
                // 	   where author.FirstName == filterValue
                // 	   select new BookAuthorModel{
                //             Id = book.Id,
                //             ISBN = book.ISBN,
                //             BookName = book.BookName,
                //             AuthorId = book.AuthorId,
                //             AuthorFirstName = author.FirstName,
                //             AuthorSurname = author.Surname,
                //             NumberOfCopies = book.NumberOfCopies,
                //             AvailableCopies = book.AvailableCopies,
                //             Genre = book.Genre
                //        })
                // 	  .AsNoTracking()
                // 	  .ToListAsync(); 

                //     return bookAuthors;
                default:
                    break;

            }

            query = query.OrderBy(book => book.BookName); // ORDER BY BookName
            var books = await query.ToListAsync();

            foreach (Book book in books)
            {
                Author? author = await _context.Authors.FindAsync(book.AuthorId);
                if (author != null && book != null)
                {
                    BookAuthorModel? bookAuthor = new()
                    {
                        Id = book.Id,
                        ISBN = book.ISBN,
                        BookName = book.BookName,
                        AuthorId = book.AuthorId,
                        AuthorFirstName = author.FirstName,
                        AuthorSurname = author.Surname,
                        NumberOfCopies = book.NumberOfCopies,
                        AvailableCopies = book.AvailableCopies,
                        Genre = book.Genre
                    };
                    bookAuthorList.Add(bookAuthor);

                }
            }

            return bookAuthorList;
        }
        public async Task<Book?> GetBookByBookId(int id)
        {
            return await _context.Books.FindAsync(id);
        }

    }


}
