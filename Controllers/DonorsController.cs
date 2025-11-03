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
    public class DonorsController : Controller
    {
        private readonly DBContext _context;

        public DonorsController(DBContext context)
        {
            _context = context;
        }

        // GET: Donors
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.Donors.Include(d => d.User);
            return View(await dBContext.ToListAsync());
        }

        // GET: Donors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donors = await _context.Donors
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.DonorID == id);
            if (donors == null)
            {
                return NotFound();
            }

            return View(donors);
        }

        // GET: Donors/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserID");
            return View();
        }

        // POST: Donors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonorID,UserID,Address,Country")] Donors donors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserID", donors.UserID);
            return View(donors);
        }

        // GET: Donors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donors = await _context.Donors.FindAsync(id);
            if (donors == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserID", donors.UserID);
            return View(donors);
        }

        // POST: Donors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonorID,UserID,Address,Country")] Donors donors)
        {
            if (id != donors.DonorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonorsExists(donors.DonorID))
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
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserID", donors.UserID);
            return View(donors);
        }

        // GET: Donors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donors = await _context.Donors
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.DonorID == id);
            if (donors == null)
            {
                return NotFound();
            }

            return View(donors);
        }

        // POST: Donors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donors = await _context.Donors.FindAsync(id);
            if (donors != null)
            {
                _context.Donors.Remove(donors);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonorsExists(int id)
        {
            return _context.Donors.Any(e => e.DonorID == id);
        }
    }
}
