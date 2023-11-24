using ourWinch.Models.Dashboard;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Represents a Hydrolisk object that is associated with a service order.
/// Hydrolisk may represent a specific type of inspection or item checklist used in a service context.
/// </summary>
public class Hydrolisk
{
    /// <summary>
    /// Gets or sets the identifier for the Hydrolisk object.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the service order identifier. This serves as a foreign key to the ServiceOrder table.
    /// </summary>
    [ForeignKey("ServiceOrder")]
    public int ServiceOrderId { get; set; }

    /// <summary>
    /// Gets or sets the order number associated with the service order.
    /// </summary>
    public int Ordrenummer { get; set; }

    /// <summary>
    /// Gets or sets the checklist item description.
    /// </summary>
    public string? ChecklistItem { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the checklist item is marked as OK.
    /// If "OK" is selected, this will be set to true.
    /// </summary>
    public bool OK { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the item should be replaced.
    /// If "Bør skiftes" is selected, this will be set to true.
    /// </summary>
    public bool BorSkiftes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the item is defective.
    /// If "Defekt" is selected, this will be set to true.
    /// </summary>
    public bool Defekt { get; set; }

    /// <summary>
    /// Gets or sets comments related to the checklist item.
    /// This is a free-text field for additional notes or comments.
    /// </summary>
    public string? Kommentar { get; set; }

    /// <summary>
    /// Navigation property for the associated ServiceOrder. This allows for lazy loading of the service order related to this Hydrolisk object.
    /// </summary>
    public ServiceOrder? ServiceOrder { get; set; }
}

/// <summary>
/// View model representing a list of Hydrolisk objects along with additional service order details.
/// </summary>
public class HydroliskListViewModel
{
    /// <summary>
    /// Gets or sets the list of Hydrolisk objects.
    /// </summary>
    public List<Hydrolisk> Hydrolisks { get; set; } = new List<Hydrolisk>();

    /// <summary>
    /// Gets or sets the detailed information about the associated ServiceOrder.
    /// </summary>
    public ServiceOrder? ServiceOrderInfo { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the ServiceOrder.
    /// </summary>
    public int ServiceOrderId { get; set; }

    /// <summary>
    /// Gets or sets the order number for the ServiceOrder.
    /// </summary>
    public int Ordrenummer { get; set; }

    /// <summary>
    /// Gets or sets the type of product.
    /// </summary>
    public string? Produkttype { get; set; }

    /// <summary>
    /// Gets or sets the year model of the product.
    /// </summary>
    public string? Årsmodell { get; set; }

    /// <summary>
    /// Gets or sets the first name of the customer.
    /// </summary>
    public string? Fornavn { get; set; }

    /// <summary>
    /// Gets or sets the last name of the customer.
    /// </summary>
    public string? Etternavn { get; set; }

    /// <summary>
    /// Gets or sets the serial number of the product.
    /// </summary>
    public string? Serienummer { get; set; }

    /// <summary>
    /// Gets or sets the current status of the ServiceOrder.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the mobile number associated with the ServiceOrder.
    /// </summary>
    public string? MobilNo { get; set; }

    /// <summary>
    /// Gets or sets the description of the fault as provided by the customer.
    /// </summary>
    public string? Feilbeskrivelse { get; set; }

    /// <summary>
    /// Gets or sets the customer's comments regarding the ServiceOrder.
    /// </summary>
    public string? KommentarFraKunde { get; set; }

    /// <summary>
    /// Gets or sets additional comments regarding the ServiceOrder.
    /// </summary>
    public string? Kommentar { get; set; }
}




