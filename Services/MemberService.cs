
using Bookish.Models;
namespace Bookish.Services
{
    public class MemberService(BookishContext context)
    {
        private readonly BookishContext _context = context;

        public async Task AddMember(Member member)
        {
            await _context.Members.AddAsync(member);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateMember(Member member)
        {
            _context.Update(member);

            await _context.SaveChangesAsync();
        }

        public async Task<Member?> GetMemberById(string id)
        {
            if (int.TryParse(id, out int idNo))
            {
                Member? member = await _context.Members.FindAsync(idNo);
                return member;
            }
            else return null;
        }

    }

}

