using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;





[Authorize]

public class ActiveServiceController : Controller
{
    private readonly AppDbContext _context;
    private const int PageSize = 10; // 


    public ActiveServiceController(AppDbContext context)
    {
        _context = context;
    }
    public static int PageSizeValue
    {
        get
        {
            return PageSize;
        }
    }

    public async Task<IActionResult> ActiveService(int page = 1)
    {
        var query = _context.ActiveServices; // ya da ActiveService ile ilgili doğru query
        var totalItems = await query.CountAsync();

        var totalPages = (int)Math.Ceiling((double)totalItems / PageSize);
        page = Math.Clamp(page, 1, totalPages); // Sayfa sayısını sınırlandır

        var activeServices = await query
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        ViewBag.TotalPages = totalPages;
        ViewBag.CurrentPage = page;

        return View(activeServices);
    }


    [HttpDelete]
    public async Task<IActionResult> DeleteService(int id)
    {
        // ActiveService tablosundan kayıt arama
        var activeService = await _context.ActiveServices.FindAsync(id);
        if (activeService == null)
        {
            return NotFound();
        }

        // ActiveService tablosundan ilgili kaydı silme
        _context.ActiveServices.Remove(activeService);
        await _context.SaveChangesAsync();

        return NoContent();
    }



}
