using ourWinch.Models.Dashboard;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Represents the service form which encompasses various tests and checks for a service order.
/// </summary>
public class ServiceSkjema
{
    /// <summary>
    /// Gets or sets the collection of Mechanical checks associated with the service order.
    /// </summary>
    public List<Mechanical> Mechanicals { get; set; }

    /// <summary>
    /// Gets or sets the collection of Hydrolisk checks associated with the service order.
    /// </summary>
    public List<Hydrolisk> Hydrolisks { get; set; }

    /// <summary>
    /// Gets or sets the collection of Electro checks associated with the service order.
    /// </summary>
    public List<Electro> Electros { get; set; }

    /// <summary>
    /// Gets or sets the collection of FunksjonsTest checks associated with the service order.
    /// </summary>
    public List<FunksjonsTest> FunksjonsTests { get; set; }

    /// <summary>
    /// Gets or sets the collection of Trykk checks associated with the service order.
    /// </summary>
    public List<Trykk> Trykks { get; set; }

    /// <summary>
    /// Gets or sets the collection of ServiceOrder entries. This can include multiple service orders for a comprehensive view.
    /// </summary>
    public List<ServiceOrder> ServiceOrders { get; set; }
}





