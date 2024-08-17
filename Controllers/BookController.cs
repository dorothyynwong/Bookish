using Microsoft.AspNetCore.Mvc;
using Bookish.ViewModels;
using Bookish.Services;
using Bookish.Interfaces;

namespace Bookish.Controllers;

public class BookController : Controller {
    private readonly IBookService _service;

    public BookController(IBookService bookService)
    {
        _service = bookService;
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

    public async Task<IActionResult> Index(string filterType="", string filterValue="") 
    {
        ViewData["BookFilter"] = filterValue;

        if (filterType != null && filterValue !=null)
        {
            filterType = filterType.ToLower();
            filterValue = filterValue.ToLower();
        }

        var bookAuthorList = await _service.FilterBooks(filterType, filterValue);
        
        if (bookAuthorList == null)
        {
            return NotFound();
        }
        return View(bookAuthorList);
    }

    public async Task<IActionResult> Details(string id) {
        var bookAuthor = await _service.GetBookAuthorById(id);
        if (bookAuthor == null)
        {
            return NotFound();
        }
        return View(bookAuthor);
    }

    public async Task<IActionResult> Edit(string id)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        BookAuthorModel? bookAuthor = await _service.GetBookAuthorById(id);
        if (bookAuthor == null)
        {
            return NotFound();
        }
        else
        {
            return View(bookAuthor);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit([Bind("Id", "ISBN","BookName", "AuthorId", "AuthorFirstName", "AuthorSurname", "NumberOfCopies", "AvailableCopies", "Genre")] BookAuthorModel bookAuthor)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        await _service.UpdateBook(bookAuthor);

        return RedirectToAction("Details", "Book", new {id = bookAuthor.Id});
    }

}
