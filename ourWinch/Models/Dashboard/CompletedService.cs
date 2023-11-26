using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ourWinch.Models.Dashboard;

/// <summary>
/// Represents a record of a service that has been completed.
/// </summary>
public class CompletedService
{
    /// <summary>
    /// Gets or sets the identifier for the CompletedService record.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the associated service order identifier.
    /// </summary>
    /// <value>
    /// The service order identifier.
    /// </value>
    public int ServiceOrderId { get; set; }

    /// <summary>
    /// Gets or sets the order number for the service.
    /// </summary>
    /// <value>
    /// The ordrenummer.
    /// </value>
    public int Ordrenummer { get; set; }

    /// <summary>
    /// Gets or sets the type of product that was serviced.
    /// </summary>
    /// <value>
    /// The produkttype.
    /// </value>
    public string Produkttype { get; set; }

    /// <summary>
    /// Gets or sets the first name of the customer who received the service.
    /// </summary>
    /// <value>
    /// The fornavn.
    /// </value>
    public string Fornavn { get; set; }

    /// <summary>
    /// Gets or sets the last name of the customer who received the service.
    /// </summary>
    /// <value>
    /// The etternavn.
    /// </value>
    public string Etternavn { get; set; }

    /// <summary>
    /// Gets or sets the date when the service was initially received.
    /// </summary>
    /// <value>
    /// The mottatt dato.
    /// </value>
    public DateTime MottattDato { get; set; }

    /// <summary>
    /// Gets or sets the description of the fault that was reported for the service.
    /// </summary>
    /// <value>
    /// The feilbeskrivelse.
    /// </value>
    public string Feilbeskrivelse { get; set; }

    /// <summary>
    /// Gets or sets the date when the service was agreed to be delivered.
    /// </summary>
    /// <value>
    /// The avtalt levering.
    /// </value>
    public DateTime AvtaltLevering { get; set; }

    /// <summary>
    /// Gets or sets the current status of the completed service.
    /// </summary>
    /// <value>
    /// The status.
    /// </value>
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets whether a service form has been created. Default is "Nei" (No).
    /// </summary>
    /// <value>
    /// The service skjema.
    /// </value>
    public string ServiceSkjema { get; set; } = "Nei";
}
