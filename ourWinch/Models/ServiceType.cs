using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ourWinch.Models
{
    public class ServiceType
    {
        [Key]   // Primary key defined.
        public int ServiceTypeId { get; set; }
      
        public int OrderId { get; set; }

        [Required]  // Not null
        public string Guarantee { get; set; }

        public DateTime ServiceTime { get; set; }

        public string Repair { get; set; }
    }
}
