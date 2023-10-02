using System.ComponentModel.DataAnnotations;

namespace ourWinch.Models
{
    public class User
    {
        [Key]   // Primary key defined.
        public int UserId { get; set; }

        [Required]
        public string Firstname { get; set; }

        public string Surname { get; set; }

        public int PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }
    }
}
