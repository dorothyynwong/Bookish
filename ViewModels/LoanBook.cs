using System.ComponentModel.DataAnnotations;

namespace Bookish.ViewModels;

   public class LoanBookModel 
    {
        public LoanBookModel() 
        {
            Id = 0;
            BookId = 0;
            ISBN = "";
            BookName = "";
            NumberOfCopies = 0;
            AvailableCopies = 0;
            DateBorrowed = DateTime.Now;
            NumberOfTimeRenewed = 0;
            IsReturned = false; 
        }

        public int Id {get; set;}

        public int BookId {get; set;}

        public string ISBN {get; set;}

        public string BookName {get; set;}

        public int NumberOfCopies {get; set;}

        public int AvailableCopies {get; set;}

        public DateTime DateBorrowed { get; set; }

        public int NumberOfTimeRenewed { get; set; }

        public bool IsReturned { get; set; }

    }
