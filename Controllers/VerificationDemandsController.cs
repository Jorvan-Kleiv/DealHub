
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DealHub.Data;
using DealHub.Models;

namespace DealHub.Controllers
{
    public class VerificationDemandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VerificationDemandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VerificationDemands
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VerificationDemand.Include(v => v.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: VerificationDemands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verificationDemand = await _context.VerificationDemand
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (verificationDemand == null)
            {
                return NotFound();
            }

            return View(verificationDemand);
        }

        // GET: VerificationDemands/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: VerificationDemands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EnterpriseName,Description,Siret,WebsiteUrl,SubmitAt,ResolvedAt,RefuseReason,AdminSolverFullName,UserId")] VerificationDemand verificationDemand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(verificationDemand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", verificationDemand.UserId);
            return View(verificationDemand);
        }

        // GET: VerificationDemands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verificationDemand = await _context.VerificationDemand.FindAsync(id);
            if (verificationDemand == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", verificationDemand.UserId);
            return View(verificationDemand);
        }

        // POST: VerificationDemands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EnterpriseName,Description,Siret,WebsiteUrl,SubmitAt,ResolvedAt,RefuseReason,AdminSolverFullName,UserId")] VerificationDemand verificationDemand)
        {
            if (id != verificationDemand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(verificationDemand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VerificationDemandExists(verificationDemand.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", verificationDemand.UserId);
            return View(verificationDemand);
        }

        // GET: VerificationDemands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verificationDemand = await _context.VerificationDemand
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (verificationDemand == null)
            {
                return NotFound();
            }

            return View(verificationDemand);
        }

        // POST: VerificationDemands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var verificationDemand = await _context.VerificationDemand.FindAsync(id);
            if (verificationDemand != null)
            {
                _context.VerificationDemand.Remove(verificationDemand);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VerificationDemandExists(int id)
        {
            return _context.VerificationDemand.Any(e => e.Id == id);
        }
    }
}
