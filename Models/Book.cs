using System.ComponentModel.DataAnnotations;

namespace Bookish.Models 
{
    public class Book 
    {
        public Book() 
        {
            Id = 0;
            ISBN = "";
            BookName = "";
            AuthorId = 0;
            NumberOfCopies = 0;
            AvailableCopies = 0;
            Genre = "";
        }

        [Key]
        public int Id {get; set;}

        [MaxLength(17)]
        [Required]
        public string ISBN {get; set;}

        [MaxLength(255)]
        [Required]
        public string BookName {get; set;}

        public int AuthorId {get; set;}

        public int NumberOfCopies {get; set;}

        public int AvailableCopies {get; set;}

        [MaxLength(35)]
        public string Genre {get; set;}
    }

}