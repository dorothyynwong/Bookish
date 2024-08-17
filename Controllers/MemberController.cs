using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;
using Bookish.Services;
using Microsoft.EntityFrameworkCore;
using Bookish.Interfaces;

namespace Bookish.Controllers;

// public class MemberController(ILogger<MemberController> logger, BookishContext context) : Controller
public class MemberController: Controller
{
    // private readonly ILogger<MemberController> _logger = logger;
    private readonly BookishContext _context;
    private readonly IMemberService _service;

    public MemberController(BookishContext context, IMemberService service)
    {
        _context = context;
        _service = service;
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

        return RedirectToAction("Index", "Member");
    }

    public async Task<IActionResult> Edit(string id)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        Member? member = await _service.GetMemberById(id);
        if (member == null)
        {
            return NotFound();
        }
        else
        {
            return View(member);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit([Bind("Id", "FirstName", "Surname", "Address", "Email", "PhoneNumber")] Member member)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        await _service.UpdateMember(member);
        
        return RedirectToAction("Index", "Member");
    }

    public async Task<IActionResult> Details(string id)
    {
        var member = await _service.GetMemberById(id);
        if (member == null)
        {
            return NotFound();
        }
        return View(member);
    }
}