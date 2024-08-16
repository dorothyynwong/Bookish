using Microsoft.AspNetCore.Mvc;
using Bookish.ViewModels;
using Bookish.Services;
using Bookish.Models;

namespace Bookish.Controllers;

public class LoanController(BookishContext context) : Controller {
    // private readonly ILogger<BookController> _logger;
    private readonly BookishContext _context = context;
    private readonly LoanServices _service = new(context);

    public async Task<IActionResult> BooksBorrowed(string id) 
    {
        var loanBookList = await _service.GetLoanBookByMemberId(id);
        if (loanBookList== null)
        {
            return NotFound();
        }
        return View(loanBookList);
    }

    public async Task<IActionResult> MembersLoaned(string id) 
    {
        var loanMemberList = await _service.GetLoanMemberByBookId(id);
        if (loanMemberList== null)
        {
            return NotFound();
        }
        return View(loanMemberList);
    }

    [HttpGet]
    public IActionResult CheckOut()
    {

        return View(); 
    }
    
    public async Task<IActionResult> CheckOut([Bind("BookId", "MemberId")] BookMemberModel bookMember)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        LoanMemberModel loanMember = await _service.CheckOut(bookMember.BookId, bookMember.MemberId);
        return View("CheckOutSuccess", loanMember);
    }

    // public IActionResult CheckOutSuccess(LoanMemberModel loanMember)
    // {
    //     return View(loanMember);

    // }

}