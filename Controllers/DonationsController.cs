using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GIFT_OF_THE_GIVERS_fOUNDATION.Data;
using GIFT_OF_THE_GIVERS_fOUNDATION.Models;

namespace GIFT_OF_THE_GIVERS_fOUNDATION.Controllers
{
    public class DonationsController : Controller
    {
        private readonly DBContext _context;

        public DonationsController(DBContext context)
        {
            _context = context;
        }

        // GET: Donations
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.Donations.Include(d => d.Donor).Include(d => d.Project);
            return View(await dBContext.ToListAsync());
        }

        // GET: Donations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donations = await _context.Donations
                .Include(d => d.Donor)
                .Include(d => d.Project)
                .FirstOrDefaultAsync(m => m.DonationID == id);
            if (donations == null)
            {
                return NotFound();
            }

            return View(donations);
        }

        // GET: Donations/Create
        public IActionResult Create()
        {
            ViewData["DonorID"] = new SelectList(_context.Donors, "DonorID", "DonorID");
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectID");
            return View();
        }

        // POST: Donations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonationID,DonorID,ProjectID,Amount,DonationType,DateDonated,PaymentMethod")] Donations donations)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DonorID"] = new SelectList(_context.Donors, "DonorID", "DonorID", donations.DonorID);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectID", donations.ProjectID);
            return View(donations);
        }

        // GET: Donations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donations = await _context.Donations.FindAsync(id);
            if (donations == null)
            {
                return NotFound();
            }
            ViewData["DonorID"] = new SelectList(_context.Donors, "DonorID", "DonorID", donations.DonorID);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectID", donations.ProjectID);
            return View(donations);
        }

        // POST: Donations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonationID,DonorID,ProjectID,Amount,DonationType,DateDonated,PaymentMethod")] Donations donations)
        {
            if (id != donations.DonationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonationsExists(donations.DonationID))
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
            ViewData["DonorID"] = new SelectList(_context.Donors, "DonorID", "DonorID", donations.DonorID);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectID", donations.ProjectID);
            return View(donations);
        }

        // GET: Donations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donations = await _context.Donations
                .Include(d => d.Donor)
                .Include(d => d.Project)
                .FirstOrDefaultAsync(m => m.DonationID == id);
            if (donations == null)
            {
                return NotFound();
            }

            return View(donations);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donations = await _context.Donations.FindAsync(id);
            if (donations != null)
            {
                _context.Donations.Remove(donations);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonationsExists(int id)
        {
            return _context.Donations.Any(e => e.DonationID == id);
        }
    }
}
