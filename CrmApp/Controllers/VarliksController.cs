using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrmApp.Models;

namespace CrmApp.Controllers
{
    public class VarliksController : Controller
    {
        private readonly CrmAppDbContext _context;

        public VarliksController(CrmAppDbContext context)
        {
            _context = context;
        }

        // GET: Varliks
        public async Task<IActionResult> Index()
        {
              return _context.Varliks != null ? 
                          View(await _context.Varliks.ToListAsync()) :
                          Problem("Entity set 'CrmAppDbContext.Varliks'  is null.");
        }

        // GET: Varliks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Varliks == null)
            {
                return NotFound();
            }

            var varlik = await _context.Varliks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (varlik == null)
            {
                return NotFound();
            }

            return View(varlik);
        }

        // GET: Varliks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Varliks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VarlikName")] Varlik varlik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(varlik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(varlik);
        }

        // GET: Varliks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Varliks == null)
            {
                return NotFound();
            }

            var varlik = await _context.Varliks.FindAsync(id);
            if (varlik == null)
            {
                return NotFound();
            }
            return View(varlik);
        }

        // POST: Varliks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VarlikName")] Varlik varlik)
        {
            if (id != varlik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(varlik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VarlikExists(varlik.Id))
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
            return View(varlik);
        }

        // GET: Varliks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Varliks == null)
            {
                return NotFound();
            }

            var varlik = await _context.Varliks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (varlik == null)
            {
                return NotFound();
            }

            return View(varlik);
        }

        // POST: Varliks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Varliks == null)
            {
                return Problem("Entity set 'CrmAppDbContext.Varliks'  is null.");
            }
            var varlik = await _context.Varliks.FindAsync(id);
            if (varlik != null)
            {
                _context.Varliks.Remove(varlik);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VarlikExists(int id)
        {
          return (_context.Varliks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
