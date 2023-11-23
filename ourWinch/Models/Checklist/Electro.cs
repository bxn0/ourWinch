using ourWinch.Models.Dashboard;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Represents an electrical item associated with a service order.
/// </summary>
public class Electro
{
    /// <summary>
    /// Gets or sets the identifier for the Electro object.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the service order identifier as a foreign key.
    /// </summary>
    [ForeignKey("ServiceOrder")]
    public int ServiceOrderId { get; set; } // Foreign key for ServiceOrder

    /// <summary>
    /// Gets or sets the order number associated with this item.
    /// </summary>
    public int Ordrenummer { get; set; }
    /// <summary>
    /// Gets or sets the checklist item description.
    /// </summary>
    public string? ChecklistItem { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the checklist item is OK.
    /// </summary>
    public bool OK { get; set; } // If "OK" is selected, this will be true
    /// <summary>
    /// Gets or sets a value indicating whether the item should be changed.
    /// </summary>
    public bool BorSkiftes { get; set; } // If "Bør skiftes" is selected, this will be true

    /// <summary>
    /// Gets or sets a value indicating whether the item is defective.
    /// </summary>
    public bool Defekt { get; set; } // If "Defekt" is selected, this will be true

    /// <summary>
    /// Gets or sets a comment about the checklist item.
    /// </summary>
    public string? Kommentar { get; set; } // Commentary field

    /// <summary>
    /// Navigation property for the associated ServiceOrder.
    /// </summary>
    public ServiceOrder? ServiceOrder { get; set; } // Navigation property
}

/// <summary>
/// View model representing a list of Electro objects along with details about the service order.
/// </summary>
public class ElectroListViewModel
{
    /// <summary>
    /// Gets or sets the list of Electro objects.
    /// </summary>
    public List<Electro> Electros { get; set; } = new List<Electro>();
    /// <summary>
    /// Gets or sets the detailed information about the ServiceOrder.
    /// </summary>
    public ServiceOrder? ServiceOrderInfo { get; set; }
    /// <summary>
    /// Gets or sets the service order identifier.
    /// </summary>
    public int ServiceOrderId { get; set; }
    /// <summary>
    /// Gets or sets the order number associated with this service order.
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
    /// Gets or sets the status of the service order.
    /// </summary>
    public string? Status { get; set; }
    /// <summary>
    /// Gets or sets the mobile number associated with the service order.
    /// </summary>
    public string? MobilNo { get; set; }
    /// <summary>
    /// Gets or sets the description of the issue as provided by the customer.
    /// </summary>
    public string? Feilbeskrivelse { get; set; }
    /// <summary>
    /// Gets or sets the comments from the customer.
    /// </summary>
    public string? KommentarFraKunde { get; set; }
    /// <summary>
    /// Gets or sets any additional comments regarding the service order.
    /// </summary>
    public string? Kommentar { get; set; }
}




