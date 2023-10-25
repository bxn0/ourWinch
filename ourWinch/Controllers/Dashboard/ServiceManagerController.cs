using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


public class ServiceManager
{
    private readonly AppDbContext _context;

    public ServiceManager(AppDbContext context)
    {
        _context = context;
    }

    public void SaveToActiveService(int serviceOrderId)
    {
        var serviceOrder = _context.ServiceOrders.FirstOrDefault(so => so.ServiceOrderId == serviceOrderId);
        if (serviceOrder != null)
        {
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
            _context.ActiveServices.Add(activeService);
            _context.SaveChanges();
        }
    }

}
