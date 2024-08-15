using Bookish.Models;
using Bookish.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bookish.Services;

public class LoanServices(BookishContext context)
{
    private readonly BookishContext _context = context;
    
    private readonly BookService _bookservice = new(context);

    private readonly MemberService _memberservice = new(context);

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

    public async Task<List<LoanBookModel>> GetListLoanBookModel(List<Loan> loans) 
    {
        List<LoanBookModel> loanBookList = [];
        foreach(Loan loan in loans)
        {
            Book? book = await _bookservice.GetBookByBookId(loan.BookId);
            LoanBookModel loanBook = new LoanBookModel{
                Id = loan.Id,
                BookId = book.Id,
                ISBN = book.ISBN,
                BookName = book.BookName,
                NumberOfCopies = book.NumberOfCopies,
                AvailableCopies = book.AvailableCopies,
                DateBorrowed = loan.DateBorrowed,
                NumberOfTimeRenewed = loan.NumberOfTimeRenewed,
                IsReturned = loan.IsReturned
            };
            loanBookList.Add(loanBook);
        }
        return loanBookList;
    }

    public async Task<List<LoanBookModel>> GetLoanBookByMemberId(string id)
    {
        List<Loan> loans = await GetLoanByMemberId(id);
        return await GetListLoanBookModel(loans);
    } 

    public async Task<List<LoanMemberModel>> GetLoanMemberByBookId(string id)
    {
        List<Loan> loans = await GetLoanByBookId(id);
        return await GetListLoanMemberModel(loans);
    }

    public async Task<List<Loan>> GetLoanByBookId(string id)
    {
        if (int.TryParse(id, out int idNo))
        {
            List<Loan> loanList = [];
            IQueryable<Loan> query = _context.Loans;
            query = query.Where(loan => loan.BookId == idNo);
            return await query.ToListAsync();
        }
        else
        {
            return null;
        }
    }

    public async Task<List<LoanMemberModel>> GetListLoanMemberModel(List<Loan> loans)
    {
        List<LoanMemberModel> loanMemberList = [];
        foreach(Loan loan in loans)
        {
            Member? member = await _memberservice.GetMemberById(loan.MemberId.ToString());
            LoanMemberModel loanMember = new LoanMemberModel{
                Id = loan.Id,
                DateBorrowed = loan.DateBorrowed,
                NumberOfTimeRenewed = loan.NumberOfTimeRenewed,
                IsReturned = loan.IsReturned,
                MemberId = member.Id,
                MemberFirstName = member.FirstName,
                MemberSurname = member.Surname,
                MemberEmail = member.Email
            };
            loanMemberList.Add(loanMember);
        }
        return loanMemberList;
    }
}