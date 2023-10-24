namespace ourWinchSist.Models
{

    public class ServiceOrder
    {
        public int ServiceOrderId { get; set; }
        public string? Fornavn { get; set; }
        public string? Etternavn { get; set; }
        public string? MobilNo { get; set; }
        public string? Email { get; set; }
        public string? Adresse { get; set; }
        public string? Feilbeskrivelse { get; set; }
        public int Ordrenummer { get; set; }
        public string? Produkttype { get; set; }
        public string? Serienummer { get; set; }
        public DateTime MottattDato { get; set; } // Tarih bilgisi DateTime türünde tutulmalıdır.
        public string? Årsmodell { get; set; }
        // Garanti, Servis ve Reperasjon için belki bool türünde bir alan kullanabilirsiniz, eğer bu alanlar evet/hayır şeklinde ise.
        public bool Garanti { get; set; }
        public bool Servis { get; set; }
        public bool Reperasjon { get; set; }
        public string? KommentarFraKunde { get; set; }



    }

}