using System.ComponentModel.DataAnnotations;

namespace Bookish.ViewModels;

   public class BookMemberModel 
    {
        // public BookMemberModel() 
        // {
        //     Id = 0;
        //     BookId = 0;
        //     ISBN = "";
        //     BookName = "";
        //     NumberOfCopies = 0;
        //     AvailableCopies = 0;
        //     MemberId = 0;
        //     MemberFirstName = "";
        //     MemberSurname = "";
        // }

        // public int Id {get; set;}

        public int BookId {get; set;}

        // public string ISBN {get; set;}

        // public string BookName {get; set;}

        // public int NumberOfCopies {get; set;}

        // public int AvailableCopies {get; set;}

        public int MemberId {get; set;}
        // public string MemberFirstName {get; set;}
        // public string MemberSurname {get; set;}

    }
