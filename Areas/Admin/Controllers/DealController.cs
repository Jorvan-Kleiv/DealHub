using DealHub.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DealHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DealController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DealController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var deals = await _context.Deal
                    .Include(d => d.Category)
                    .Include(d => d.Merchant)
                    .Include(d => d.User)
                    .OrderByDescending(d => d.CreatedAt)
                    .ToListAsync();

            return View(deals);
        }
    }
}
