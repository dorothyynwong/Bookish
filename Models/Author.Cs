using System.ComponentModel.DataAnnotations;

namespace Bookish.Models
{
    public class Author 
    {
        public Author()
        {
            Id = 0;
            FirstName = "";
            Surname = "";
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(70)]
        public string FirstName {get; set;}

        [MaxLength(35)]
        public string Surname {get; set;}
    }
}