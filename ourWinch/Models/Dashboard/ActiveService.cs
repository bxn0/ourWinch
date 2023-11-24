using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ourWinch.Models.Dashboard;

/// <summary>
/// Represents an active service record in the system.
/// </summary>
public class ActiveService
{
    /// <summary>
    /// Gets or sets the identifier for the ActiveService record.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the service order identifier. This is a required field and acts as a foreign key.
    /// </summary>
    [Required]
    [ForeignKey("ServiceOrder")]
    public int ServiceOrderId { get; set; }

    /// <summary>
    /// Navigation property for the related ServiceOrder.
    /// </summary>
    public ServiceOrder ServiceOrder { get; set; }

    /// <summary>
    /// Gets or sets the order number for the service.
    /// </summary>
    public int Ordrenummer { get; set; }

    /// <summary>
    /// Gets or sets the type of product being serviced. This is a required field.
    /// </summary>
    [Required]
    public string? Produkttype { get; set; }

    /// <summary>
    /// Gets or sets the first name of the customer. This is a required field.
    /// </summary>
    [Required]
    public string? Fornavn { get; set; }

    /// <summary>
    /// Gets or sets the last name of the customer. This is a required field.
    /// </summary>
    [Required]
    public string? Etternavn { get; set; }

    /// <summary>
    /// Gets or sets the date the service was received. This is a required field.
    /// </summary>
    [Required]
    public DateTime MottattDato { get; set; }

    /// <summary>
    /// Gets or sets the description of the issue provided by the customer. This is a required field.
    /// </summary>
    [Required]
    public string? Feilbeskrivelse { get; set; }

    /// <summary>
    /// Gets or sets the agreed upon delivery date for the service.
    /// </summary>
    public DateTime AvtaltLevering { get; set; }

    /// <summary>
    /// Gets or sets the current status of the service.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets whether a service form has been created. Default is "Nei" (No).
    /// </summary>
    public string? ServiceSkjema { get; set; } = "Nei";
}
