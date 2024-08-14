using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;
using Microsoft.EntityFrameworkCore;
using Bookish.ViewModels;
using Bookish.Services;

namespace Bookish.Controllers;

public class BookController(BookishContext context) : Controller {
    // private readonly ILogger<BookController> _logger;
    private readonly BookishContext _context = context;
    private readonly BookService _service = new(context);

    // public BookController(ILogger<BookController> logger, BookishContext context)

    public async Task<IActionResult> Index() {
        var books = await _context.Books.ToListAsync();
        if (books == null)
        {
            return NotFound();
        }
        return View(books);
    }

    public async Task<IActionResult> Create([Bind("ISBN","BookName", "AuthorFirstName", "AuthorSurname", "NumberOfCopies", "AvailableCopies", "Genre")] BookAuthorModel bookAuthor)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        await _service.AddBookAuthor(bookAuthor);

        return RedirectToAction("Index", "Book");
    }

    public async Task<IActionResult> BookList(string genre) {
        var bookAuthorList = await _service.GetBookAuthorByGenre(genre);
        if (bookAuthorList == null)
        {
            return NotFound();
        }
        return View(bookAuthorList);
    }
}