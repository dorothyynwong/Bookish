using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookish.Controllers;

public class AuthorController : Controller 
{
    private readonly ILogger<AuthorController> _logger;
    private BookishContext _context;

    public AuthorController(ILogger<AuthorController> logger, BookishContext context)
    {
        _logger = logger;
        _context = context;
    }
    // public IActionResult Index()
    // {
        
    //     return View(author1);
    // }
    // public IActionResult Index()
    // {
    //     Author author = new Author {FirstName = "John", Surname = "Smith"};
    //     return View(author);
    // }

    public async Task<IActionResult> Index(int id)
    {   
        // if (Id == null)
        // {
        //     return NotFound();
        // }

        // var author = await _context.Authors.FindAsync(Id);
        // Id = 1;
        var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        if (author == null)
        {
            return NotFound();
        }
        return View(author);
    }
}