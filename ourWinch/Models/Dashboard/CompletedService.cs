using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ourWinch.Models.Dashboard;

public class CompletedService
{
    public int Id { get; set; }

    [Required]
    [ForeignKey("ServiceOrder")]
    public int ServiceOrderId { get; set; }

    public ServiceOrder ServiceOrder { get; set; }

    public int Ordrenummer { get; set; }

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

    public string? Status { get; set; }

    public string? ServiceSkjema { get; set; } = "Nei";
}
