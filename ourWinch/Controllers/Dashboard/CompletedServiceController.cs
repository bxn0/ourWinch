using ourWinch.Models;
using ourWinch.Models.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Logger için gerekli namespace
using System.Diagnostics;
using System.Linq;

[Authorize]
public class CompletedServiceController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<CompletedServiceController> _logger; // Logger nesnesi

    // Dependency injection ile AppDbContext ve ILogger nesnelerini al
    public CompletedServiceController(AppDbContext context, ILogger<CompletedServiceController> logger)
    {
        _context = context;
        _logger = logger; // Enjekte edilen logger nesnesini ata
    }

    // GET: Gösterilen dashboard
    // GET: Dashboard/CompletedService
    public IActionResult Index()
    {
        var completedServices = _context.CompletedServices.ToList();
        return View("~/Views/Dashboard/CompletedService.cshtml", completedServices);
    }

    // POST: Fullført tjeneste kaydı için
    [HttpPost]
    public IActionResult RegisterCompletedService(CompletedService completedService)
    {
        if (ModelState.IsValid)
        {
            // Model doğruysa, veritabanına ekle
            _context.CompletedServices.Add(completedService);
            _context.SaveChanges(); // Değişiklikleri kaydet
            return RedirectToAction("Index", "CompletedService");
        }

        // ModelState geçerli değilse, hataları logla
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            _logger.LogError(error.ErrorMessage); // Hata mesajlarını logla
        }

        // Hatalı model ile Error view'ını döndür
        var errorViewModel = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };
        return View("Error", errorViewModel);
    }
}
