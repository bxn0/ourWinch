using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ActiveServiceController : Controller
{
    private readonly AppDbContext _context;
    private const int PageSize = 10; // Her sayfada gösterilecek öğe sayısı.

    public ActiveServiceController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> ActiveService(int page = 1)
    {
        if (page <= 0)
        {
            return BadRequest("Invalid page number.");
        }

        var totalItems = await _context.ServiceOrders.CountAsync();
        if (totalItems == 0)
        {
            throw new Exception("ServiceOrders tablosunda kayıt yok!");
        }

        var totalPages = (int)Math.Ceiling((double)totalItems / PageSize);

        if (page <= 0 || page > totalPages)
        {
            throw new Exception($"Geçersiz sayfa numarası: {page}. Toplam sayfa sayısı: {totalPages}");
        }

        {
            return BadRequest("Page number exceeds total page count.");
        }

        var serviceOrders = await _context.ServiceOrders
          .Skip((page - 1) * PageSize)
          .Take(PageSize)
          .ToListAsync();

        if (!serviceOrders.Any())
        {
            throw new Exception("ServiceOrders sorgusu boş sonuç döndürdü!");
        }

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;

        return View(serviceOrders);
    }

}
