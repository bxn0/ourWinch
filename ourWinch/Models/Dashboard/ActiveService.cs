using ourWinch.Models.Dashboard;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ActiveService
{
    public int Id { get; set; }

    [Required]
    [ForeignKey("ServiceOrder")]
    public int Ordrenummer { get; set; }

    public virtual ServiceOrder? ServiceOrder { get; set; }

    [Required]
    public string? Produkttype { get; set; }

    [Required]
    public string? Fornavn { get; set; }

    [Required]
    public string? Etternavn { get; set; }

    [Required]
    public DateTime MottattDato { get; set; }

    [Required]
    public string? Feilbeskrivelse { get; set; }

  
    public DateTime AvtaltLevering { get; set; }

    [Required]
    public string? Status { get; set; }


    public string? ServiceSkjema { get; set; }

    // Diğer gereken özellikleri de buraya ekleyebilirsiniz...
}
