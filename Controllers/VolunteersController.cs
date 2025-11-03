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
    public class VolunteersController : Controller
    {
        private readonly DBContext _context;

        public VolunteersController(DBContext context)
        {
            _context = context;
        }

        // GET: Volunteers
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.Volunteers.Include(v => v.User);
            return View(await dBContext.ToListAsync());
        }

        // GET: Volunteers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteers = await _context.Volunteers
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.VolunteerID == id);
            if (volunteers == null)
            {
                return NotFound();
            }

            return View(volunteers);
        }

        // GET: Volunteers/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserID");
            return View();
        }

        // POST: Volunteers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VolunteerID,UserID,Skills,Availability,Location")] Volunteers volunteers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(volunteers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserID", volunteers.UserID);
            return View(volunteers);
        }

        // GET: Volunteers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteers = await _context.Volunteers.FindAsync(id);
            if (volunteers == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserID", volunteers.UserID);
            return View(volunteers);
        }

        // POST: Volunteers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VolunteerID,UserID,Skills,Availability,Location")] Volunteers volunteers)
        {
            if (id != volunteers.VolunteerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(volunteers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolunteersExists(volunteers.VolunteerID))
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
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserID", volunteers.UserID);
            return View(volunteers);
        }

        // GET: Volunteers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteers = await _context.Volunteers
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.VolunteerID == id);
            if (volunteers == null)
            {
                return NotFound();
            }

            return View(volunteers);
        }

        // POST: Volunteers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volunteers = await _context.Volunteers.FindAsync(id);
            if (volunteers != null)
            {
                _context.Volunteers.Remove(volunteers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolunteersExists(int id)
        {
            return _context.Volunteers.Any(e => e.VolunteerID == id);
        }
    }
}
