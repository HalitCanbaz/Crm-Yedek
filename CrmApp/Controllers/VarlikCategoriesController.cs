using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrmApp.Models;
using CrmApp.ViewModel.VarlikCategoriesViewModels;

namespace CrmApp.Controllers
{
    public class VarlikCategoriesController : Controller
    {
        private readonly CrmAppDbContext _context;

        public VarlikCategoriesController(CrmAppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int Id)
        {
            var varlikCategoriList = _context.Varlikcategories
                .Join(_context.Varliks, x => x.VarlikId, y => y.Id, (x, y)
                => new { Varlikcategories = x, Varliks = y })
                .Join(_context.Categories, x => x.Varlikcategories.CategoriesId, y => y.Id, (x, y)
                => new { x.Varlikcategories, x.Varliks, Categories = y })
                .Join(_context.Users, x => x.Varliks.AppUserId, y => y.Id, (x, y)
                => new { x.Varlikcategories, x.Varliks, x.Categories, Users = y }).Select(x => new ListOfVCViewModel
                {
                    UserId = x.Users.Id,
                    UserName = x.Users.NameSurName,
                    VarlikName = x.Varliks.VarlikName,
                    CategoryName = x.Categories.CategoryName
                }).Where(x => x.UserId == Id);

            var list = varlikCategoriList.Select(x => new ListOfVCViewModel()
            {
                UserName = x.UserName,
                VarlikName = x.VarlikName,
                CategoryName = x.CategoryName
            });

            return View(list.ToList());
        }


        public IActionResult Create(int Id)
        {
            var varliks = _context.Varliks.Where(x => x.Id == Id);
            var user = _context.Users.Join(_context.Varliks, x => x.Id, y => y.AppUserId, (x, y)
                => new { Users = x, Varliks = y }).Where(x => x.Varliks.Id == Id).FirstOrDefault();



            ViewData["CategoriesId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            ViewData["VarlikId"] = new SelectList(varliks, "Id", "VarlikName");


            var result = new CreateVCViewModel()
            {
                NameSurName = user.Users.NameSurName

            };

            return View(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Id, CreateVCViewModel model)
        {
            var varliks =await  _context.Varliks.Where(x => x.Id == Id).FirstOrDefaultAsync();          


            var varlik = await _context.Varliks.FindAsync(model.VarlikId);
            var category = await _context.Categories.FindAsync(model.CategoriesId);

            var varlikCategories = new VarlikCategories()
            {
                VarlikId = model.VarlikId,
                CategoriesId = model.CategoriesId,
                VarlikCategoriesName = varlik.VarlikName + "-" + category.CategoryName,

            };

            _context.Add(varlikCategories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = varliks.AppUserId });

        }



    }
}
