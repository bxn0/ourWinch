using System.ComponentModel.DataAnnotations;

namespace ourWinch.Models
{
    public class Bruker
    {
        [Key]   // primary key tanimlandi.
        public int BrukerId { get; set; }

        [Required]
        public string Fornavn { get; set; }


        public string Etternavn { get; set; }


        public string Mobil_no { get; set;}

        public string Email { get; set; }  

        public string Adresse{ get; set; } 

        public string Post_nummer { get; set; }  

        



    }
}
