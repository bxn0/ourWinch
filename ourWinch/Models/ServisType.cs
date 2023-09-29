using System.ComponentModel.DataAnnotations;

namespace ourWinch.Models
{
    public class ServisType
    {
        [Key]   // primary key tanimlandi.

        public int ServisTypeId { get; set; }

        public int OrderId { get; set; }

        [Required]  // not null
        public string Garanti { get; set; }


        public string ServisTid { get; set; }


        public string Reperasjon { get; set;}

        

    }
}
