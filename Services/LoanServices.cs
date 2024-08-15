using Bookish.Models;
using Bookish.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bookish.Services;

public class LoanServices(BookishContext context)
{
    private readonly BookishContext _context = context;

    public async Task AddLoan(Loan loan)
    {
        await _context.Loans.AddAsync(loan);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Loan>> GetLoanByMemberId(string id)
    {
        if (int.TryParse(id, out int idNo))
        {
            List<BookMemberModel> bookMemberList = [];
            IQueryable<Loan> query = _context.Loans;
            query = query.Where(loan => loan.MemberId == idNo);
            return await query.ToListAsync();
            // 1. Get a list of loans by member ID
            // 2. Foreach loan, get the book ID from loan => book details from Books
            

            
            // var loanQuery = _context.Loans.Join(_context.Books,
            //             loan => loan.MemberId,
            //             book => book.MemberId,
            //             (loan, book) => new BookMemberModel
            //             {
            //                 Id = loan.Id,
            //                 BookId = loan.BookId,
            //                 ISBN = book.ISBN,
            //                 BookName = book.BookName,
            //                 NumberOfCopies = book.NumberOfCopies,
            //                 AvailableCopies = book.AvailableCopies,
                            
            //             })
            //             .Where(loan => loan.BookId == idNo)
            //             .OrderBy(book => book.BookName);
            //             return await loanQuery.ToListAsync();
        }
        else return null;
    }

    public async Task<List<BookMemberModel>> GetListBookMemberModel(List<Loan> loans) 
    {
        List<BookMemberModel> bookMemberList = [];
        foreach(Loan loan in loans)
        {
            bookMemberList.Add(GetBookByBookId(loan.BookId));
        }
    }

    public async Task GetLoanByMemberID(string id)
    {

    }
}