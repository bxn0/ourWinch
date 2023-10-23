using Microsoft.Build.Framework;

namespace ourWinch.Models.Dashboard
{

    public class ServiceOrder
    {

        public int ServiceOrderId { get; set; }
        [Required]
        public string? Fornavn { get; set; }

        [Required]
        public string? Etternavn { get; set; }

        public string? MobilNo { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Adresse { get; set; }

        [Required]
        public string? Feilbeskrivelse { get; set; }
        public int Ordrenummer { get; set; }
        public string? Produkttype { get; set; }

        [Required]
        public string? Serienummer { get; set; }

        [Required]
        public DateTime MottattDato { get; set; }

    
        public string? Årsmodell { get; set; }

        [Required]        
        public bool Garanti { get; set; }
        public bool Servis { get; set; }
        public bool Reperasjon { get; set; }
        public string? KommentarFraKunde { get; set; }
        public string? Status { get; internal set; }
    }
}