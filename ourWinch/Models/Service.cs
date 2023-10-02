using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ourWinch.Models
{
    public class Service
    {
        [Key]   // Primary key defined.
        public int ServiceId { get; set; }

        
      
        public int OrderId { get; set; }

        public string CustomerName { get; set; }

        public string ProductType { get; set; }

        public DateTime AgreedDelivery { get; set; }

      
        public string ServiceStatus { get; set; }

        public string ServiceForm { get; set; }

        public int LaborHours { get; set; }
    }
}
