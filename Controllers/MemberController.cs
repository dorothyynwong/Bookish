using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookish.Controllers;

public class MemberController : Controller {
    private readonly ILogger<MemberController> _logger;
    private BookishContext _context;

    public MemberController(ILogger<MemberController> logger, BookishContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index() {
        var members = await _context.Members.ToListAsync();
        if (members == null)
        {
            return NotFound();
        }
        return View(members);
    }
}