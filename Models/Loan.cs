using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bookish.Models 
{
    public class Loan 
    {
        public Loan()
        {
            int Id = 0;
            int BookId = 0;
            int MemberId = 0;
            DateTime DateBorrowed = DateTime.Now;
            int NumberOfTimeRenewed = 0;
        }

        [Key]
        public int Id {get; set;}

        [ForeignKey("Book")]
        public int BookId {get; set;}
        public Book Book { get; set; }
        
        [ForeignKey("Member")]
        public int MemberId {get; set;}
        public Member Member { get; set; }

        public DateTime DateBorrowed {get; set;}

        public int NumberOfTimeRenewed { get; set; }
    }
}