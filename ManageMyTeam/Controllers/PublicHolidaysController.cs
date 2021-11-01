/*
 * Controller Projekt
 * 10.2021 Cédric Brunner
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManageMyTeam.Data;
using ManageMyTeam.Models;

namespace ManageMyTeam.Controllers
{
    public class PublicHolidaysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PublicHolidaysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PublicHolidays
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PublicHolidays.Include(b => b.Site);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PublicHolidays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicHoliday = await _context.PublicHolidays
                .Include(b => b.Site)
                .FirstOrDefaultAsync(m => m.PublicHolidayId == id);
            if (publicHoliday == null)
            {
                return NotFound();
            }

            return View(publicHoliday);
        }

        // GET: PublicHolidays/Create
        public IActionResult Create()
        {
            ViewData["SiteId"] = new SelectList(_context.Sites, "SiteId", "SiteLocation");
            return View();
        }

        // POST: PublicHolidays/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PublicHolidayId,PublicHolidayTitle,PublicHolidayDate,SiteId")] PublicHoliday publicHoliday)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publicHoliday);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SiteId"] = new SelectList(_context.Sites, "SiteId", "Siteid", publicHoliday.SiteId);
            return View(publicHoliday);
        }

        // GET: PublicHolidays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicHoliday = await _context.PublicHolidays.FindAsync(id);
            if (publicHoliday == null)
            {
                return NotFound();
            }
            ViewData["SiteId"] = new SelectList(_context.Sites, "SiteId", "SiteLocation", publicHoliday.SiteId);
            return View(publicHoliday);
        }

        // POST: PublicHolidays/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PublicHolidayId,PublicHolidayTitle,PublicHolidayDate,SiteId")] PublicHoliday publicHoliday)
        {
            if (id != publicHoliday.PublicHolidayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publicHoliday);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicHolidayExists(publicHoliday.PublicHolidayId))
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
            ViewData["Siteid"] = new SelectList(_context.Sites, "SiteId", "SiteLocation", publicHoliday.SiteId);
            return View(publicHoliday);
        }

        // GET: PublicHolidays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicHoliday = await _context.PublicHolidays
                .Include(b => b.Site)
                .FirstOrDefaultAsync(m => m.PublicHolidayId == id);
            if (publicHoliday == null)
            {
                return NotFound();
            }

            return View(publicHoliday);
        }

        // POST: PublicHolidays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publicHoliday = await _context.PublicHolidays.FindAsync(id);
            _context.PublicHolidays.Remove(publicHoliday);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicHolidayExists(int id)
        {
            return _context.PublicHolidays.Any(e => e.PublicHolidayId == id);
        }
    }
}
