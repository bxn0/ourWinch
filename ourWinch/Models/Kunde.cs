using System.ComponentModel.DataAnnotations;

namespace ourWinch.Models
{
    public class Kunde
    {
        [Key]   // primary key tanimlandi.
        public int KundeId { get; set; }

        [Required]  // not null
        public string Fornavn { get; set; }


        public string Etternavn { get; set; }


        public int MobilNo { get; set;}

        public string Email { get; set; }  

        public string Adress{ get; set; } 

        public string PostNummer { get; set; }  

        



    }
}
