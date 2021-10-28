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
    public class RequirementHoursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequirementHoursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RequirementHours
        /*public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RequirementHours.Include(r => r.Department).Include(r => r.Project);
            return View(await applicationDbContext.ToListAsync());
        }*/


        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["DepartmentSortParm"] = String.IsNullOrEmpty(sortOrder) ? "department_desc" : "";
            ViewData["ProjectSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Project_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            var requestHours = from s in _context.RequirementHours.Include(r => r.Department).Include(r => r.Project)
                               select s;
            switch (sortOrder)
            {
                case "department_desc":
                    requestHours = requestHours.OrderByDescending(s => s.Department);
                    break;
                case "Project_desc":
                    requestHours = requestHours.OrderByDescending(s => s.Project);
                    break;
                case "Date":
                    requestHours = requestHours.OrderBy(s => s.RequirementHourDate);
                    break;
                case "date_desc":
                    requestHours = requestHours.OrderByDescending(s => s.RequirementHourDate);
                    break;
                default:
                    requestHours = requestHours.OrderBy(s => s.Department);
                    break;
            }
            return View(await requestHours.AsNoTracking().ToListAsync());
        }



        // GET: RequirementHours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requirementHour = await _context.RequirementHours
                .Include(r => r.Department)
                .Include(r => r.Project)
                .FirstOrDefaultAsync(m => m.RequirementHourId == id);
            if (requirementHour == null)
            {
                return NotFound();
            }

            return View(requirementHour);
        }

        // GET: RequirementHours/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectTitle");
            return View();
        }

        // POST: RequirementHours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequirementHourId,RequirementHourAmount,RequirementHourDate,ProjectId,DepartmentId")] RequirementHour requirementHour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requirementHour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", requirementHour.DepartmentId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", requirementHour.ProjectId);
            return View(requirementHour);
        }

        // GET: RequirementHours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requirementHour = await _context.RequirementHours.FindAsync(id);
            if (requirementHour == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", requirementHour.DepartmentId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectTitle", requirementHour.ProjectId);
            return View(requirementHour);
        }

        // POST: RequirementHours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequirementHourId,RequirementHourAmount,RequirementHourDate,ProjectId,DepartmentId")] RequirementHour requirementHour)
        {
            if (id != requirementHour.RequirementHourId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requirementHour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequirementHourExists(requirementHour.RequirementHourId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", requirementHour.DepartmentId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", requirementHour.ProjectId);
            return View(requirementHour);
        }

        // GET: RequirementHours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requirementHour = await _context.RequirementHours
                .Include(r => r.Department)
                .Include(r => r.Project)
                .FirstOrDefaultAsync(m => m.RequirementHourId == id);
            if (requirementHour == null)
            {
                return NotFound();
            }

            return View(requirementHour);
        }

        // POST: RequirementHours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var requirementHour = await _context.RequirementHours.FindAsync(id);
            _context.RequirementHours.Remove(requirementHour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequirementHourExists(int id)
        {
            return _context.RequirementHours.Any(e => e.RequirementHourId == id);
        }
    }
}
