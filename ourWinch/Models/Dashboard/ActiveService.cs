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
    /// <value>
    /// The identifier.
    /// </value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the service order identifier. This is a required field and acts as a foreign key.
    /// </summary>
    /// <value>
    /// The service order identifier.
    /// </value>
    [Required]
    [ForeignKey("ServiceOrder")]
    public int ServiceOrderId { get; set; }

    /// <summary>
    /// Navigation property for the related ServiceOrder.
    /// </summary>
    /// <value>
    /// The service order.
    /// </value>
    public ServiceOrder ServiceOrder { get; set; }

    /// <summary>
    /// Gets or sets the order number for the service.
    /// </summary>
    /// <value>
    /// The ordrenummer.
    /// </value>
    public int Ordrenummer { get; set; }

    /// <summary>
    /// Gets or sets the type of product being serviced. This is a required field.
    /// </summary>
    /// <value>
    /// The produkttype.
    /// </value>
    [Required]
    public string? Produkttype { get; set; }

    /// <summary>
    /// Gets or sets the first name of the customer. This is a required field.
    /// </summary>
    /// <value>
    /// The fornavn.
    /// </value>
    [Required]
    public string? Fornavn { get; set; }

    /// <summary>
    /// Gets or sets the last name of the customer. This is a required field.
    /// </summary>
    /// <value>
    /// The etternavn.
    /// </value>
    [Required]
    public string? Etternavn { get; set; }

    /// <summary>
    /// Gets or sets the date the service was received. This is a required field.
    /// </summary>
    /// <value>
    /// The mottatt dato.
    /// </value>
    [Required]
    public DateTime MottattDato { get; set; }

    /// <summary>
    /// Gets or sets the description of the issue provided by the customer. This is a required field.
    /// </summary>
    /// <value>
    /// The feilbeskrivelse.
    /// </value>
    [Required]
    public string? Feilbeskrivelse { get; set; }

    /// <summary>
    /// Gets or sets the agreed upon delivery date for the service.
    /// </summary>
    /// <value>
    /// The avtalt levering.
    /// </value>
    public DateTime AvtaltLevering { get; set; }

    /// <summary>
    /// Gets or sets the current status of the service.
    /// </summary>
    /// <value>
    /// The status.
    /// </value>
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets whether a service form has been created. Default is "Nei" (No).
    /// </summary>
    /// <value>
    /// The service skjema.
    /// </value>
    public string? ServiceSkjema { get; set; } = "Nei";
}
