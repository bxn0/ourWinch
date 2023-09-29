using System.ComponentModel.DataAnnotations;

namespace ourWinch.Models
{
    public class Service
    {
        [Key]   // primary key tanimlandi.
        public int ServisId { get; set; }

        [Required]  // not null
        public int OrderId { get; set; }

        public string KundeNavn { get; set; }

        public string ProductType { get; set; }


        public decimal AvtaltLevering { get; set; }


        public string ServisStatus { get; set;}

        public string ServisSkjema { get; set; }  

        public  int JobTimer { get; set; } 

       


    }
}
