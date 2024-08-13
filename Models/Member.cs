using System.ComponentModel.DataAnnotations;

namespace Bookish.Models
{
    public class Member 
    {
        public Member()
        {

        }

        [Key]
        public int Id { get; set; }

        [MaxLength(70)]
        public string FirstName {get; set;}

        [MaxLength(35)]
        public string Surname {get; set;}  
        
        [MaxLength(175)]
        public string Address {get; set;} 

        public int PhoneNumber {get; set;}

        [MaxLength(320)]
        public string Email {get; set;}           
    }
}