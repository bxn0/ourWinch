using System.ComponentModel.DataAnnotations;

namespace ourWinch.Models
{
    public class Order
    {
        [Key]   // primary key tanimlandi.
        public int OrderId { get; set; }

        [Required]  // not null
        public string KundeId { get; set; }

        public string ProductType { get; set; }


        public DateTime MottattDato { get; set;}

        public int Årsmodel{ get; set; }  

        public string KommentarFelte{ get; set; } 

        



    }
}
