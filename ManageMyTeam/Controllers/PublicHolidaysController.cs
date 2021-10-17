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
            return View(await _context.PublicHolidays.ToListAsync());
        }

        // GET: PublicHolidays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicHoliday = await _context.PublicHolidays
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
            return View();
        }

        // POST: PublicHolidays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PublicHolidayId,PublicHolidayTitle,PublicHolidayDate")] PublicHoliday publicHoliday)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publicHoliday);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            return View(publicHoliday);
        }

        // POST: PublicHolidays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PublicHolidayId,PublicHolidayTitle,PublicHolidayDate")] PublicHoliday publicHoliday)
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
