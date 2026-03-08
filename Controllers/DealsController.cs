using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DealHub.Data;
using DealHub.Models;

namespace DealHub.Controllers
{
    public class DealsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DealsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Deals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Deal.Include(d => d.Category).Include(d => d.Merchant).Include(d => d.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Deals/Details/5
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
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Id");
            ViewData["MerchantId"] = new SelectList(_context.Set<Merchant>(), "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Deals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Status,Url,ImageUrl,OriginalPrice,FinalPrice,VoteScore,UserId,CategoryId,MerchantId")] Deal deal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Id", deal.CategoryId);
            ViewData["MerchantId"] = new SelectList(_context.Set<Merchant>(), "Id", "Id", deal.MerchantId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", deal.UserId);
            return View(deal);
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

        // POST: Deals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Status,Url,ImageUrl,OriginalPrice,FinalPrice,VoteScore,UserId,CategoryId,MerchantId")] Deal deal)
        {
            if (id != deal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DealExists(deal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Id", deal.CategoryId);
            ViewData["MerchantId"] = new SelectList(_context.Set<Merchant>(), "Id", "Id", deal.MerchantId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", deal.UserId);
            return View(deal);
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
