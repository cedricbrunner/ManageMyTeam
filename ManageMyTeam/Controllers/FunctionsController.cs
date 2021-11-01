/*
 * Controller Funktion
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
    public class FunctionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FunctionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Functions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Functions.ToListAsync());
        }

        // GET: Functions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var function = await _context.Functions
                .FirstOrDefaultAsync(m => m.FunctionId == id);
            if (function == null)
            {
                return NotFound();
            }

            return View(function);
        }

        // GET: Functions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Functions/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FunctionId,FunctionTyp")] Function function)
        {
            if (ModelState.IsValid)
            {
                _context.Add(function);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(function);
        }

        // GET: Functions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var function = await _context.Functions.FindAsync(id);
            if (function == null)
            {
                return NotFound();
            }
            return View(function);
        }

        // POST: Functions/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FunctionId,FunctionTyp")] Function function)
        {
            if (id != function.FunctionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(function);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FunctionExists(function.FunctionId))
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
            return View(function);
        }

        // GET: Functions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var function = await _context.Functions
                .FirstOrDefaultAsync(m => m.FunctionId == id);
            if (function == null)
            {
                return NotFound();
            }

            return View(function);
        }

        // POST: Functions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var function = await _context.Functions.FindAsync(id);
            _context.Functions.Remove(function);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FunctionExists(int id)
        {
            return _context.Functions.Any(e => e.FunctionId == id);
        }
    }
}
