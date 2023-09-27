using System.ComponentModel.DataAnnotations;

namespace ourWinch.Models
{
    public class FulførteService
    {
        [Key]   // primary key tanimlandi.
        public int OrderId { get; set; }

        [Required]  // not null
        public string ProductType { get; set; }


        public string Kunde { get; set; }


        public string MottattDato { get; set;}

        public string FeilBeskriving { get; set; }  

        public string AvtaltLevering { get; set; } 

        public string Status { get; set; }  

        public string ServiceSkjema { get; set; }


        public int Timer { get; set; }



    }
}
