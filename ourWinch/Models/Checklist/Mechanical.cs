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

    public string? Kommentar { get; set; }
}




