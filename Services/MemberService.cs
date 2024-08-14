
using Bookish.Models;
namespace Bookish.Services
{
    public class MemberService
    {
        private BookishContext _context;
        // private readonly ILogger<MemberService> _logger;
        public MemberService(BookishContext context)
        {
            _context = context;
        }

        public async Task AddMember(Member member)
        {
            await _context.Members.AddAsync(member);

            await _context.SaveChangesAsync();
        }

    }

}

