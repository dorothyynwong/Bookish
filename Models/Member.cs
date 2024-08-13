using System.ComponentModel.DataAnnotations;

namespace Bookish.Models
{
    public class Member 
    {
        public Member()
        {
            FirstName = "";
            Surname = "";
            Address = "";
            PhoneNumber = 0;
            Email = "";
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(70)]
        [Required]
        public string FirstName {get; set;}

        [MaxLength(35)]
        [Required]
        public string Surname {get; set;}  
        
        [MaxLength(175)]
        [Required]
        public string Address {get; set;} 

        public int PhoneNumber {get; set;}

        [MaxLength(320)]
        [Required]
        public string Email {get; set;}           
    }
}