using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ourWinch.Models
{
    public class Order
    {
        [Key]   // Primary key defined.
        public int OrderId { get; set; }

        [Required]  // Not null

    
        public int CustomerId { get; set; }

        public string ProductType { get; set; }

        public DateTime ReceivedDate { get; set; }

        public int YearModel { get; set; }

        public string CommentField { get; set; }
    }
}
