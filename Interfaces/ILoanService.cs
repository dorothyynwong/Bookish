using Bookish.Models;
using Bookish.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bookish.Interfaces 
{
    public interface ILoanService
    {
        Task AddLoan(Loan loan);
        Task<List<Loan>> GetLoanByMemberId(string id);
        Task<List<LoanBookModel>> GetListLoanBookModel(List<Loan> loans);
        Task<List<LoanBookModel>> GetLoanBookByMemberId(string id); 
        Task<List<LoanMemberModel>> GetLoanMemberByBookId(string id);
        Task<List<Loan>> GetLoanByBookId(string id);
        Task<List<LoanMemberModel>> GetListLoanMemberModel(List<Loan> loans);
        Task<LoanMemberModel> CheckOut(int memberId, int bookId);
        
    }

}