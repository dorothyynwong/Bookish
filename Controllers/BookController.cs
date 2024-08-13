using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookish.Controllers;

public class BookController : Controller {
    private readonly ILogger<BookController> _logger;
    private BookishContext _context;

    public BookController(ILogger<BookController> logger, BookishContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index() {
        var books = await _context.Books.Select().ToList();
        if (books == null)
        {
            return NotFound();
        }
        return View(books);
    }
}