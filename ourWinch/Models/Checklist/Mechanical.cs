using ourWinch.Models.Dashboard;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Represents a mechanical item associated with a service order, detailing its status and any notes.
/// </summary>
public class Mechanical
{
    /// <summary>
    /// Gets or sets the identifier for the Mechanical object.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the service order identifier as a foreign key.
    /// </summary>
    /// <value>
    /// The service order identifier.
    /// </value>
    [ForeignKey("ServiceOrder")]
    public int ServiceOrderId { get; set; } // Foreign key for ServiceOrder

    /// <summary>
    /// Gets or sets the order number associated with this mechanical item.
    /// </summary>
    /// <value>
    /// The ordrenummer.
    /// </value>
    public int Ordrenummer { get; set; }

    /// <summary>
    /// Gets or sets the checklist item description.
    /// </summary>
    /// <value>
    /// The checklist item.
    /// </value>
    public string? ChecklistItem { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the checklist item is OK.
    /// If "OK" is selected, this property is set to true.
    /// </summary>
    /// <value>
    ///   <c>true</c> if ok; otherwise, <c>false</c>.
    /// </value>
    public bool OK { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the item should be replaced.
    /// If "Bør skiftes" is selected, this property is set to true.
    /// </summary>
    /// <value>
    ///   <c>true</c> if [bor skiftes]; otherwise, <c>false</c>.
    /// </value>
    public bool BorSkiftes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the item is defective.
    /// If "Defekt" is selected, this property is set to true.
    /// </summary>
    /// <value>
    ///   <c>true</c> if defekt; otherwise, <c>false</c>.
    /// </value>
    public bool Defekt { get; set; }

    /// <summary>
    /// Gets or sets any comments or notes regarding the checklist item.
    /// </summary>
    /// <value>
    /// The kommentar.
    /// </value>
    public string? Kommentar { get; set; } // Commentary field

    /// <summary>
    /// Navigation property for the associated ServiceOrder.
    /// </summary>
    /// <value>
    /// The service order.
    /// </value>
    public ServiceOrder? ServiceOrder { get; set; } // Navigation property
}


/// <summary>
/// View model representing a list of Mechanical objects along with associated service order details.
/// </summary>
public class MechanicalListViewModel
{
    /// <summary>
    /// Gets or sets the list of Mechanical objects.
    /// </summary>
    /// <value>
    /// The mechanicals.
    /// </value>
    public List<Mechanical> Mechanicals { get; set; } = new List<Mechanical>();

    /// <summary>
    /// Gets or sets the identifier for the ServiceOrder from which details are derived.
    /// </summary>
    /// <value>
    /// The service order identifier.
    /// </value>
    public int ServiceOrderId { get; set; }

    /// <summary>
    /// Gets or sets the order number associated with the ServiceOrder.
    /// </summary>
    /// <value>
    /// The ordrenummer.
    /// </value>
    public int Ordrenummer { get; set; }

    /// <summary>
    /// Gets or sets the product type from the ServiceOrder.
    /// </summary>
    /// <value>
    /// The produkttype.
    /// </value>
    public string? Produkttype { get; set; }

    /// <summary>
    /// Gets or sets the year model of the product from the ServiceOrder.
    /// </summary>
    /// <value>
    /// The årsmodell.
    /// </value>
    public string? Årsmodell { get; set; }

    /// <summary>
    /// Gets or sets the first name of the customer from the ServiceOrder.
    /// </summary>
    /// <value>
    /// The fornavn.
    /// </value>
    public string? Fornavn { get; set; }

    /// <summary>
    /// Gets or sets the last name of the customer from the ServiceOrder.
    /// </summary>
    /// <value>
    /// The etternavn.
    /// </value>
    public string? Etternavn { get; set; }

    /// <summary>
    /// Gets or sets the serial number of the product from the ServiceOrder.
    /// </summary>
    /// <value>
    /// The serienummer.
    /// </value>
    public string? Serienummer { get; set; }

    /// <summary>
    /// Gets or sets the current status of the ServiceOrder.
    /// </summary>
    /// <value>
    /// The status.
    /// </value>
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the mobile number associated with the ServiceOrder.
    /// </summary>
    /// <value>
    /// The mobil no.
    /// </value>
    public string? MobilNo { get; set; }

    /// <summary>
    /// Gets or sets the fault description provided by the customer in the ServiceOrder.
    /// </summary>
    /// <value>
    /// The feilbeskrivelse.
    /// </value>
    public string? Feilbeskrivelse { get; set; }

    /// <summary>
    /// Gets or sets the customer's comments from the ServiceOrder.
    /// </summary>
    /// <value>
    /// The kommentar fra kunde.
    /// </value>
    public string? KommentarFraKunde { get; set; }

    /// <summary>
    /// Gets or sets any additional comments regarding the ServiceOrder.
    /// </summary>
    /// <value>
    /// The kommentar.
    /// </value>
    public string? Kommentar { get; set; }
}




