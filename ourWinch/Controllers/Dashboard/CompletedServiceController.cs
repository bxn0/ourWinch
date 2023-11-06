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
    [HttpPost]
    public IActionResult RegisterCompletedService(CompletedService completedService)
    {
        if (ModelState.IsValid)
        {
            // Önce ilgili ActiveService kaydını bul
            var activeService = _context.ActiveServices.FirstOrDefault(a => a.ServiceOrderId == completedService.ServiceOrderId);
            // Eğer varsa ve daha önce tamamlanmamışsa, işlemleri yap
            if (activeService != null && activeService.Status != "Completed")
            {
                // Model doğruysa, veritabanına ekle
                _context.CompletedServices.Add(completedService);
                // ActiveService kaydını sil
                _context.ActiveServices.Remove(activeService);
                _context.SaveChanges(); // Değişiklikleri kaydet
                return RedirectToAction("Index", "CompletedService");
            }
        }
        // ModelState geçerli değilse veya işlem zaten tamamlanmışsa, hataları logla
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            _logger.LogError(error.ErrorMessage); // Hata mesajlarını logla
        }
        // Hatalı model veya zaten tamamlanmış işlem ile Error view'ını döndür
        return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
