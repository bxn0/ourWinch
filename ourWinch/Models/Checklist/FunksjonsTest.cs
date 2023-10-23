using ourWinch.Models.Dashboard;
using System.ComponentModel.DataAnnotations.Schema;

public class FunksjonsTest
{
        public int Id { get; set; }
    [ForeignKey("ServiceOrder")]
    public int ServiceOrderId { get; set; } // Foreign key for ServiceOrder
    public int Ordrenummer { get; set; }
        public string? ChecklistItem { get; set; }
        public bool OK { get; set; } // Eğer "OK" seçilirse true olacak
        public bool BorSkiftes { get; set; } // Eğer "Bør skiftes" seçilirse true olacak
        public bool Defekt { get; set; } // Eğer "Defekt" seçilirse true olacak
        public string? Kommentar { get; set; } // Yorum alanı

   
    public ServiceOrder? ServiceOrder { get; set; } // Navigation property
 
}

public class FunksjonsTestListViewModel
{
    public List<FunksjonsTest> FunksjonsTests { get; set; } = new List<FunksjonsTest>();
    public ServiceOrder? ServiceOrderInfo { get; set; }
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




