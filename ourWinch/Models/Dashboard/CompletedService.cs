using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ourWinch.Models.Dashboard;

public class CompletedService
{
    public int Id { get; set; }

    public int ServiceOrderId { get; set; }

    public int Ordrenummer { get; set; }

   
    public string Produkttype { get; set; }

   
    public string Fornavn { get; set; }

    
    public string Etternavn { get; set; }

    
    public DateTime MottattDato { get; set; }

   
    public string Feilbeskrivelse { get; set; }

    public DateTime AvtaltLevering { get; set; }

    public string Status { get; set; }

    public string ServiceSkjema { get; set; } = "Nei";
}
