using ourWinchSist.Models;

public class Electro
{
        public int Id { get; set; }

    public int ServiceOrderId { get; set; } // Foreign key for ServiceOrder
    public int Ordrenummer { get; set; }
        public string? ChecklistItem { get; set; }
        public bool OK { get; set; } // Eğer "OK" seçilirse true olacak
        public bool BorSkiftes { get; set; } // Eğer "Bør skiftes" seçilirse true olacak
        public bool Defekt { get; set; } // Eğer "Defekt" seçilirse true olacak
        public string? Kommentar { get; set; } // Yorum alanı

   
    public ServiceOrder? ServiceOrder { get; set; } // Navigation property
}

public class ElectroListViewModel
{
    public List<Electro> Electros { get; set; } = new List<Electro>();
    public ServiceOrder? ServiceOrderInfo { get; set; }
    public string? Kommentar { get; set; }
}




