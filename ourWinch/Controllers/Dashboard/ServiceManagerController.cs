using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// A service layer class responsible for saving service orders to active service order.
/// </summary>
[Authorize]
public class ServiceManager
{
    /// <summary>
    /// The database context used for database operations.
    /// </summary>
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceManager"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public ServiceManager(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Saves a service order to the active services.
    /// </summary>
    /// <param name="serviceOrderId">The ID of the service order to be activated.</param>
    public void SaveToActiveService(int serviceOrderId)
    {
        // Retrieve the service order by its ID.
        var serviceOrder = _context.ServiceOrders.FirstOrDefault(so => so.ServiceOrderId == serviceOrderId);
        if (serviceOrder != null)
        {
            // Create a new active service based on the service order details.
            var activeService = new ActiveService
            {
                ServiceOrderId = serviceOrder.ServiceOrderId,  
                Ordrenummer = serviceOrder.Ordrenummer,
                Produkttype = serviceOrder.Produkttype,
                Fornavn = serviceOrder.Fornavn,
                Etternavn = serviceOrder.Etternavn,
                MottattDato = serviceOrder.MottattDato,
                Feilbeskrivelse = serviceOrder.Feilbeskrivelse,
                Status = "Process",
            };
            // Add the new active service to the database and save changes.
            _context.ActiveServices.Add(activeService);
            _context.SaveChanges();
        }
    }

}
