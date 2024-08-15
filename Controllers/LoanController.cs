using Microsoft.AspNetCore.Mvc;
using Bookish.ViewModels;
using Bookish.Services;

namespace Bookish.Controllers;

public class LoanController(BookishContext context) : Controller {
    // private readonly ILogger<BookController> _logger;
    private readonly BookishContext _context = context;
    private readonly LoanServices _service = new(context);


}