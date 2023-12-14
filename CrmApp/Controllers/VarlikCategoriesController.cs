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
    public class VarlikCategoriesController : Controller
    {
        private readonly CrmAppDbContext _context;

        public VarlikCategoriesController(CrmAppDbContext context)
        {
            _context = context;
        }

        // GET: VarlikCategories
        public async Task<IActionResult> Index()
        {
            var crmAppDbContext = _context.Varlikcategories.Include(v => v.Categories).Include(v => v.Varlik);
            return View(await crmAppDbContext.ToListAsync());
        }

        // GET: VarlikCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Varlikcategories == null)
            {
                return NotFound();
            }

            var VarlikCategories = await _context.Varlikcategories
                .Include(v => v.Categories)
                .Include(v => v.Varlik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (VarlikCategories == null)
            {
                return NotFound();
            }

            return View(VarlikCategories);
        }

        // GET: VarlikCategories/Create
        public IActionResult Create()
        {
            ViewData["CategoriesId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            ViewData["VarlikId"] = new SelectList(_context.Varliks, "Id", "VarlikName");
            return View();
        }

        // POST: VarlikCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VarlikCategories model)
        {
            var varlik = await _context.Varliks.FindAsync(model.VarlikId);
            var category = await _context.Categories.FindAsync(model.CategoriesId);

            var varlikCategories = new VarlikCategories()
            {
                VarlikId = model.VarlikId,
                CategoriesId = model.CategoriesId,
                VarlikCategoriesName = varlik.VarlikName + "-" + category.CategoryName
            };

            _context.Add(varlikCategories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //ViewData["CategoriesId"] = new SelectList(_context.VarlikCategories, "Id", "CategoryName", VarlikCategories.CategoriesId);
            //ViewData["VarlikId"] = new SelectList(_context.Varliks, "Id", "VarlikName", VarlikCategories.VarlikId);
            //return View(VarlikCategories);
        }

        // GET: VarlikCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Varlikcategories == null)
            {
                return NotFound();
            }

            var VarlikCategories = await _context.Varlikcategories.FindAsync(id);
            if (VarlikCategories == null)
            {
                return NotFound();
            }
            ViewData["CategoriesId"] = new SelectList(_context.Categories, "Id", "CategoryName", VarlikCategories.CategoriesId);
            ViewData["VarlikId"] = new SelectList(_context.Varliks, "Id", "VarlikName", VarlikCategories.VarlikId);
            return View(VarlikCategories);
        }

        // POST: VarlikCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VarlikId,CategoriesId")] VarlikCategories model)
        {


            var varlik = await _context.Varliks.FindAsync(model.VarlikId);
            var category = await _context.Categories.FindAsync(model.CategoriesId);
            var findVarlikCategory = await _context.Varlikcategories.Where(x => x.Id == id).FirstOrDefaultAsync();


            if (findVarlikCategory != null)
            {
                findVarlikCategory.VarlikId = model.VarlikId;
                findVarlikCategory.CategoriesId = model.CategoriesId;
                findVarlikCategory.VarlikCategoriesName = varlik.VarlikName + "-" + category.CategoryName;
                await _context.SaveChangesAsync();

            }

            return RedirectToAction(nameof(Index));





            //if (id != VarlikCategories.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(VarlikCategories);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!VarlikCategoriesExists(VarlikCategories.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CategoriesId"] = new SelectList(_context.Categories, "Id", "CategoryName", VarlikCategories.CategoriesId);
            //ViewData["VarlikId"] = new SelectList(_context.Varliks, "Id", "VarlikName", VarlikCategories.VarlikId);
            //return View(VarlikCategories);
        }

        // GET: VarlikCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Varlikcategories == null)
            {
                return NotFound();
            }

            var VarlikCategories = await _context.Varlikcategories
                .Include(v => v.Categories)
                .Include(v => v.Varlik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (VarlikCategories == null)
            {
                return NotFound();
            }

            return View(VarlikCategories);
        }

        // POST: VarlikCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Varlikcategories == null)
            {
                return Problem("Entity set 'CrmAppDbContext.Varlikcategories'  is null.");
            }
            var VarlikCategories = await _context.Varlikcategories.FindAsync(id);
            if (VarlikCategories != null)
            {
                _context.Varlikcategories.Remove(VarlikCategories);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VarlikCategoriesExists(int id)
        {
            return (_context.Varlikcategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
