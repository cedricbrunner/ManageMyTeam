/*
 * Conreoller Absenzen
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
    public class AbsencesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AbsencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Absences
        // Sort

        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["StartDateSortParm"] = sortOrder == "Date" ? "startdate_desc" : "Date";


            var absences = from s in _context.Absences.Include(a => a.EmployeeName)
                               select s;
            switch (sortOrder)
            {
                case "name_desc":
                    absences = absences.OrderByDescending(s => s.EmployeeName);
                    break;
                case "Date":
                    absences = absences.OrderBy(s => s.AbcenceStart);
                    break;
                case "startdate_desc":
                    absences = absences.OrderByDescending(s => s.AbcenceStart);
                    break;
                default:
                    absences = absences.OrderBy(s => s.EmployeeName);
                    break;
            }
            return View(await absences.AsNoTracking().ToListAsync());
        }


        // GET: Absences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absence = await _context.Absences
                .Include(a => a.EmployeeName)
                .FirstOrDefaultAsync(m => m.AbsenceId == id);
            if (absence == null)
            {
                return NotFound();
            }

            return View(absence);
        }

        // GET: Absences/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName");
            return View();
        }

        // POST: Absences/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AbsenceId,AbcenceName,AbcenceStart,AbcenceEnd,EmployeeId")] Absence absence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(absence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", absence.EmployeeId);
            return View(absence);
        }

        // GET: Absences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absence = await _context.Absences.FindAsync(id);
            if (absence == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName", absence.EmployeeId);
            return View(absence);
        }

        // POST: Absences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AbsenceId,AbcenceName,AbcenceStart,AbcenceEnd,EmployeeId")] Absence absence)
        {
            if (id != absence.AbsenceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(absence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbsenceExists(absence.AbsenceId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName", absence.EmployeeId);
            return View(absence);
        }

        // GET: Absences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absence = await _context.Absences
                .Include(a => a.EmployeeName)
                .FirstOrDefaultAsync(m => m.AbsenceId == id);
            if (absence == null)
            {
                return NotFound();
            }

            return View(absence);
        }

        // POST: Absences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var absence = await _context.Absences.FindAsync(id);
            _context.Absences.Remove(absence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbsenceExists(int id)
        {
            return _context.Absences.Any(e => e.AbsenceId == id);
        }
    }
}
