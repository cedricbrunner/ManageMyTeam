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
    public class BaseLoadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BaseLoadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BaseLoads
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Baseloads.Include(b => b.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BaseLoads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baseLoad = await _context.Baseloads
                .Include(b => b.Employee)
                .FirstOrDefaultAsync(m => m.BaseLoadId == id);
            if (baseLoad == null)
            {
                return NotFound();
            }

            return View(baseLoad);
        }

        // GET: BaseLoads/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName");
            return View();
        }

        // POST: BaseLoads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BaseLoadId,BaseLoadTitle,BaseLoadAmount,EmployeeId")] BaseLoad baseLoad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baseLoad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", baseLoad.EmployeeId);
            return View(baseLoad);
        }

        // GET: BaseLoads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baseLoad = await _context.Baseloads.FindAsync(id);
            if (baseLoad == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName", baseLoad.EmployeeId);
            return View(baseLoad);
        }

        // POST: BaseLoads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BaseLoadId,BaseLoadTitle,BaseLoadAmount,EmployeeId")] BaseLoad baseLoad)
        {
            if (id != baseLoad.BaseLoadId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baseLoad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaseLoadExists(baseLoad.BaseLoadId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName", baseLoad.EmployeeId);
            return View(baseLoad);
        }

        // GET: BaseLoads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baseLoad = await _context.Baseloads
                .Include(b => b.Employee)
                .FirstOrDefaultAsync(m => m.BaseLoadId == id);
            if (baseLoad == null)
            {
                return NotFound();
            }

            return View(baseLoad);
        }

        // POST: BaseLoads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baseLoad = await _context.Baseloads.FindAsync(id);
            _context.Baseloads.Remove(baseLoad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaseLoadExists(int id)
        {
            return _context.Baseloads.Any(e => e.BaseLoadId == id);
        }
    }
}
