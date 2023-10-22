using ourWinch.Models.Dashboard;

public class Mechanical
{
        public int Id { get; set; }
        public int Ordrenummer { get; set; }
        public string? ChecklistItem { get; set; }
        public bool OK { get; set; } // Eğer "OK" seçilirse true olacak
        public bool BorSkiftes { get; set; } // Eğer "Bør skiftes" seçilirse true olacak
        public bool Defekt { get; set; } // Eğer "Defekt" seçilirse true olacak
        public string? Kommentar { get; set; } // Yorum alanı

    public int ServiceOrderId { get; set; } // Foreign key for ServiceOrder
    public ServiceOrder? ServiceOrder { get; set; } // Navigation property
}

public class MechanicalListViewModel
{
    public List<Mechanical> Mechanicals { get; set; } = new List<Mechanical>();

         // ServiceOrder'dan alınacak bilgiler
        public int ServiceOrderId { get; set; }
        public int Ordrenummer { get; set; }
        public string? Produkttype { get; set; }
        public string Årsmodell { get; set; }
        public string? Fornavn { get; set; }
        public string? Etternavn { get; set; }
        public string? Serienummer { get; set; }
        public string? Status { get; set; }
        public string MobilNo { get; set; }
        public string? Feilbeskrivelse { get; set; }
        public string? KommentarFraKunde { get; set; }
        public string? Kommentar { get; set; }
    }




