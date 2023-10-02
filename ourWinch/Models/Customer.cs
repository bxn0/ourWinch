using System.ComponentModel.DataAnnotations;

namespace ourWinch.Models
{
    public class Customer
    {
        [Key]   // Primary key defined.
        public int CustomerId { get; set; }

        [Required]  // Not null
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int MobileNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }
    }
}
