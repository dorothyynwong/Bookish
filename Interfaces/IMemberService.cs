using Bookish.Models;

namespace Bookish.Interfaces
{
    public interface IMemberService
    {
        Task AddMember(Member member);
        Task UpdateMember(Member member);
        Task<List<Member>> GetMembers();
        Task<Member?> GetMemberById(string id);
        

    }
}