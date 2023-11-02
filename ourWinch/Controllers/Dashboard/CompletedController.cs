// Controllers/FulførteController.cs
using ourWinch.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]

public class CompletedController : Controller
{
    public IActionResult Dashboard()
    {
        return View("~/Views/Dashboard/Completed.cshtml");
    }

    private readonly AppDbContext _context;

    public CompletedController(AppDbContext context)
    {
        _context = context;
    }

    // Controllers/FulførteController.cs
    [HttpPost]
    public async Task<IActionResult> CompleteService(int id)
    {
        var activeService = await _context.ActiveServices.FindAsync(id);
        if (activeService == null)
        {
            return NotFound();
        }

        // CompletedService nesnesini oluştur ve değerleri ata
        var completedService = new CompletedService
        {
            ServiceOrderId = activeService.ServiceOrderId,
            Ordrenummer = activeService.Ordrenummer,
            Produkttype = activeService.Produkttype,
            Fornavn = activeService.Fornavn,
            Etternavn = activeService.Etternavn,
            MottattDato = activeService.MottattDato,
            Feilbeskrivelse = activeService.Feilbeskrivelse,
            AvtaltLevering = activeService.AvtaltLevering,
            Status = activeService.Status,
            ServiceSkjema = activeService.ServiceSkjema
        };

        // Kayıtları ilgili tablolara ekle/sil
        _context.CompletedServices.Add(completedService);
        _context.ActiveServices.Remove(activeService);
        await _context.SaveChangesAsync();

        return RedirectToAction("Dashboard");
    }


}

