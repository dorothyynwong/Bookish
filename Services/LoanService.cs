using Bookish.Models;
using Bookish.ViewModels;
using Bookish.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookish.Services;

public class LoanService: ILoanService
{
    private readonly BookishContext _context;
    private readonly IBookService _bookService;
    private readonly IMemberService _memberService;
    public LoanService(BookishContext context, IBookService bookService, IMemberService memberService) 
    {
        _context = context;
        _bookService = bookService;
        _memberService = memberService;
    }

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
            Book? book = await _bookService.GetBookByBookId(loan.BookId);
            LoanBookModel loanBook = new LoanBookModel{
                Id = loan.Id,
                BookId = book.Id,
                ISBN = book.ISBN,
                BookName = book.BookName,
                NumberOfCopies = book.NumberOfCopies,
                AvailableCopies = book.AvailableCopies,
                DateBorrowed = loan.DateBorrowed.ToUniversalTime(),
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
            Member? member = await _memberService.GetMemberById(loan.MemberId.ToString());
            LoanMemberModel loanMember = new LoanMemberModel{
                Id = loan.Id,
                DateBorrowed = loan.DateBorrowed.ToUniversalTime(),
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

    public async Task<LoanMemberModel> CheckOut(int memberId, int bookId)
    { // 1. Add loan record (member Id, book Id)
    // 2. Update book record
        Loan loan = new Loan{
            BookId = bookId,
            MemberId = memberId,
            DateBorrowed = DateTime.Now.ToUniversalTime(),
            NumberOfTimeRenewed = 0,
            IsReturned = false
        };
        await AddLoan(loan);

        Book book = await _bookService.UpdateBookCopy(bookId, true);
        Member member = await _memberService.GetMemberById(memberId.ToString());

        LoanMemberModel loanMember = new LoanMemberModel {
                Id = loan.Id,
                DateBorrowed = loan.DateBorrowed.ToUniversalTime(),
                NumberOfTimeRenewed = loan.NumberOfTimeRenewed,
                IsReturned = loan.IsReturned,
                MemberId = memberId,
                MemberFirstName = member.FirstName,
                MemberSurname = member.Surname,
                MemberEmail = member.Email
        };

        return loanMember;
        
    }

    // public async Task<Loan> UpdateLoan(memberId, bookId)
    // {
    //    List<Loan> memberLoans = GetLoanByMemberId(memberId); 
    //    Loan loan = memberLoans.Find(new Loan{BookId = bookId});
    //    loan.IsReturned = true;
       
    // //    foreach(loan in memberLoans)
    // //    {
    // //     memberLoans.BookId == bookId
    // //    }
    // // }
    // public async Task<LoanMemberModel> CheckIn(int memberId, int bookId)
    // { 
    //     UpdateLoan(memberId, bookId);

    //     Book book = await _bookservice.UpdateBookCopy(bookId, true);
    //     Member member = await _memberservice.GetMemberById(memberId.ToString());

    //     LoanMemberModel loanMember = new LoanMemberModel {
    //             Id = loan.Id,
    //             DateBorrowed = loan.DateBorrowed,
    //             NumberOfTimeRenewed = loan.NumberOfTimeRenewed,
    //             IsReturned = loan.IsReturned,
    //             MemberId = memberId,
    //             MemberFirstName = member.FirstName,
    //             MemberSurname = member.Surname,
    //             MemberEmail = member.Email
    //     };

    //     return loanMember;
        
    // }
} 