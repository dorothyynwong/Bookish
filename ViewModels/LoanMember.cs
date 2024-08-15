using System.ComponentModel.DataAnnotations;

namespace Bookish.ViewModels;

   public class LoanMemberModel 
    {
        public LoanMemberModel() 
        {
            Id = 0;
            DateBorrowed = DateTime.Now;
            NumberOfTimeRenewed = 0;
            IsReturned = false; 
            MemberId = 0;
            MemberFirstName = "";
            MemberSurname = "";
            MemberEmail = "";
        }

        public int Id {get; set;}

        public DateTime DateBorrowed { get; set; }

        public int NumberOfTimeRenewed { get; set; }

        public bool IsReturned { get; set; }

        public int MemberId {get; set;}

        public string MemberFirstName {get; set;}

        public string MemberSurname {get; set;}

        public string MemberEmail {get; set;}
    }
