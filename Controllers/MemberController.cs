using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;
using Bookish.Services;
using Microsoft.EntityFrameworkCore;

namespace Bookish.Controllers;

public class MemberController : Controller
{
    private readonly ILogger<MemberController> _logger;
    private BookishContext _context;

    private MemberService _service;

    public MemberController(ILogger<MemberController> logger, BookishContext context)
    {
        _logger = logger;
        _context = context;
        _service = new MemberService(context);
    }

    public async Task<IActionResult> Index()
    {
        var members = await _context.Members.ToListAsync();
        if (members == null)
        {
            return NotFound();
        }
        return View(members);
    }

    public async Task<IActionResult> Create([Bind("FirstName", "Surname", "Address", "Email", "PhoneNumber")] Member member)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        await _service.AddMember(member);

        // await _context.Members.AddAsync(member);

        // await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Member");
    }

    public async Task<IActionResult> Edit(string id)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        Member member = await _context.Members.FindAsync(int.Parse(id));
        
        return View(member);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, [Bind("Id", "FirstName", "Surname", "Address", "Email", "PhoneNumber")] Member member)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        // Member editMember = await _context.Members.FindAsync(int.Parse(id));

        //  _context.Attach(editMember).State = EntityState.Modified;
        _context.Update(member);

        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Member");
    }
}