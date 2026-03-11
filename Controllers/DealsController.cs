using Azure.Core;
using DealHub.Data;
using DealHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealHub.Controllers
{
    [Authorize]
    public class DealsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DealsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Deal.Include(d => d.Category).Include(d => d.Merchant).Include(d => d.User);
            var categories = await _context.Category.ToListAsync();
            var merchants = await _context.Merchants.ToListAsync();
            var dealDto = new DealDto();
            dealDto.Deals = await _context.Deal.ToListAsync();
            dealDto.Categories = categories;
            dealDto.Merchants = merchants;
            return View(dealDto);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deal = await _context.Deal
                .Include(d => d.Category)
                .Include(d => d.Merchant)
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deal == null)
            {
                return NotFound();
            }

            return View(deal);
        }

        // GET: Deals/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {

            var model = new DealRequest
            {
                Categories = await _context.Set<Category>().Where(c => c.IsActive).ToListAsync(),
                Merchants = await _context.Set<Merchant>().ToListAsync(),
                Deal = new Deal()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(DealRequest request)
        {
            var userId = _userManager.GetUserId(User);
            if (userId is null) return RedirectToAction("Login", "Account");
            ModelState.Remove("Categories");
            ModelState.Remove("Merchants");
            ModelState.Remove("ImageFile");
            ModelState.Remove("Deal.ImageUrl");   // ✅ géré après upload
            ModelState.Remove("Deal.Category");   // ✅ nav property, pas besoin de valider
            ModelState.Remove("Deal.CreatedAt");
            if (ModelState.IsValid)
            {
                if (request.ImageFile is { Length: > 0 })
                {
                    var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "deals");
                    Directory.CreateDirectory(uploadDir);

                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.ImageFile.FileName)}";
                    var filePath = Path.Combine(uploadDir, fileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await request.ImageFile.CopyToAsync(stream);

                    request.Deal.ImageUrl = $"/uploads/deals/{fileName}";
                }
                foreach (var error in ModelState)
                {
                    if (error.Value.Errors.Count > 0)
                        Console.WriteLine($"❌ {error.Key}: {error.Value.Errors[0].ErrorMessage}");
                }
                request.Deal.UserId = userId;
                request.Deal.CreatedAt = DateTime.Now;
                _context.Add(request.Deal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            request.Categories = _context.Category?.ToList() ?? new List<Category>();
            request.Merchants = _context.Merchants?.ToList() ?? new List<Merchant>();
            return View(request);
        }

        // GET: Deals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deal = await _context.Deal.FindAsync(id);
            if (deal == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Id", deal.CategoryId);
            ViewData["MerchantId"] = new SelectList(_context.Set<Merchant>(), "Id", "Id", deal.MerchantId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", deal.UserId);
            return View(deal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DealRequest request)
        {
            if (id != request.Deal.Id) return NotFound();

            ModelState.Remove("Categories");
            ModelState.Remove("Merchants");
            ModelState.Remove("ImageFile");
            ModelState.Remove("Deal.ImageUrl");
            ModelState.Remove("Deal.Category");
            ModelState.Remove("Deal.CreatedAt");
            ModelState.Remove("Deal.UserId");
            ModelState.Remove("Deal.User");

            if (ModelState.IsValid)
            {
                try
                {
                    if (request.ImageFile is { Length: > 0 })
                    {
                        var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "deals");
                        Directory.CreateDirectory(uploadDir);
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.ImageFile.FileName)}";
                        var filePath = Path.Combine(uploadDir, fileName);
                        using var stream = new FileStream(filePath, FileMode.Create);
                        await request.ImageFile.CopyToAsync(stream);
                        request.Deal.ImageUrl = $"/uploads/deals/{fileName}";
                    }
                    else
                    {
                        var existing = await _context.Deal.AsNoTracking()
                                             .FirstOrDefaultAsync(d => d.Id == id);
                        request.Deal.ImageUrl = existing?.ImageUrl;
                    }

                    var existing2 = await _context.Deal.AsNoTracking()
                                         .FirstOrDefaultAsync(d => d.Id == id);
                    request.Deal.UserId = existing2?.UserId;

                    _context.Update(request.Deal);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DealExists(request.Deal.Id)) return NotFound();
                    throw;
                }
            }
            request.Categories = await _context.Category.ToListAsync();
            request.Merchants = await _context.Merchants.ToListAsync();
            return View(request);
        }

        // GET: Deals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deal = await _context.Deal
                .Include(d => d.Category)
                .Include(d => d.Merchant)
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deal == null)
            {
                return NotFound();
            }

            return View(deal);
        }

        // POST: Deals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deal = await _context.Deal.FindAsync(id);
            if (deal != null)
            {
                _context.Deal.Remove(deal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DealExists(int id)
        {
            return _context.Deal.Any(e => e.Id == id);
        }
    }
    
}
