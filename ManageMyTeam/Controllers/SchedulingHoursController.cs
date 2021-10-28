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
    public class SchedulingHoursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchedulingHoursController(ApplicationDbContext context)
        {
            _context = context;
        }

        /*// GET: SchedulingHours
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SchedulingHours.Include(s => s.Employee).Include(s => s.Project);
            return View(await applicationDbContext.ToListAsync());
        }*/

        

        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["EmployeeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "employee_desc" : "";
            ViewData["ProjectSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Project_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            
            var schedulingHours = from s in _context.SchedulingHours.Include(s => s.Employee).Include(s => s.Project)
            select s;
            switch (sortOrder)
            {
                case "employee_desc":
                    schedulingHours = schedulingHours.OrderByDescending(s => s.Employee);
                    break;
                case "Project_desc":
                    schedulingHours = schedulingHours.OrderByDescending(s => s.Project);
                    break;
                case "Date":
                    schedulingHours = schedulingHours.OrderBy(s => s.SchedulingHourDate);
                    break;
                case "date_desc":
                    schedulingHours = schedulingHours.OrderByDescending(s => s.SchedulingHourDate);
                    break;
                default:
                    schedulingHours = schedulingHours.OrderBy(s => s.Employee);
                    break;
            }
            return View(await schedulingHours.AsNoTracking().ToListAsync());
        }

        // GET: SchedulingHours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedulingHour = await _context.SchedulingHours
                .Include(s => s.Employee)
                .Include(s => s.Project)
                .FirstOrDefaultAsync(m => m.SchedulingHourId == id);
            if (schedulingHour == null)
            {
                return NotFound();
            }

            return View(schedulingHour);
        }

        // GET: SchedulingHours/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectTitle");
            return View();
        }

        // POST: SchedulingHours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SchedulingHourId,SchedulingHourAmount,SchedulingHourDate,ProjectId,EmployeeId")] SchedulingHour schedulingHour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedulingHour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", schedulingHour.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", schedulingHour.ProjectId);
            return View(schedulingHour);
        }

        // GET: SchedulingHours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedulingHour = await _context.SchedulingHours.FindAsync(id);
            if (schedulingHour == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName", schedulingHour.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectTitle", schedulingHour.ProjectId);
            return View(schedulingHour);
        }

        // POST: SchedulingHours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SchedulingHourId,SchedulingHourAmount,SchedulingHourDate,ProjectId,EmployeeId")] SchedulingHour schedulingHour)
        {
            if (id != schedulingHour.SchedulingHourId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedulingHour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchedulingHourExists(schedulingHour.SchedulingHourId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName", schedulingHour.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectTitle", schedulingHour.ProjectId);
            return View(schedulingHour);
        }

        // GET: SchedulingHours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedulingHour = await _context.SchedulingHours
                .Include(s => s.Employee)
                .Include(s => s.Project)
                .FirstOrDefaultAsync(m => m.SchedulingHourId == id);
            if (schedulingHour == null)
            {
                return NotFound();
            }

            return View(schedulingHour);
        }

        // POST: SchedulingHours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedulingHour = await _context.SchedulingHours.FindAsync(id);
            _context.SchedulingHours.Remove(schedulingHour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchedulingHourExists(int id)
        {
            return _context.SchedulingHours.Any(e => e.SchedulingHourId == id);
        }
    }
}
