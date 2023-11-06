using ourWinch.Models;
using ourWinch.Models.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

[Authorize]
public class CompletedServiceController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<CompletedServiceController> _logger;
    private const int PageSize = 7;

    public CompletedServiceController(AppDbContext context, ILogger<CompletedServiceController> logger)
    {
        _context = context;
        _logger = logger;
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

        // İlgili ActiveService kaydını bul
        var activeService = _context.ActiveServices
                                    .FirstOrDefault(a => a.ServiceOrderId == completedService.ServiceOrderId);

        // Eğer varsa, ActiveService kaydını sil
        if (activeService != null)
        {
            _context.ActiveServices.Remove(activeService);
            _context.SaveChanges(); // Silme işlemini kaydet
        }

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

    [HttpGet]
    public async Task<IActionResult> Index(int page = 1)
    {
        var totalItems = await _context.CompletedServices.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalItems / PageSize);
        page = Math.Clamp(page, 1, totalPages);

        var completedServices = await _context.CompletedServices
            .OrderByDescending(cs => cs.MottattDato)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;

        return View("~/Views/Dashboard/CompletedService.cshtml", completedServices);
    }


}
