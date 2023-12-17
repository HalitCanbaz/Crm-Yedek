using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrmApp.Models;
using CrmApp.ViewModel.VarlikViewModels;
using CrmApp.ViewModel.WorkViewModels;
using Microsoft.AspNetCore.Identity;

namespace CrmApp.Controllers
{
    public class VarliksController : Controller
    {
        private readonly CrmAppDbContext _context;


        public VarliksController(CrmAppDbContext context)
        {
            _context = context;
        }


        public IActionResult Create()
        {

            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "NameSurName");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVarlikViewModel model)
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "NameSurName");

            if (!ModelState.IsValid)
            {
                return View();
            }


            Varlik varlik = new Varlik()
            {
                VarlikName = model.VarlikName,
                VarlikCode = model.VarlikCode,
                VarlikDescription = model.VarlikDescription,
                AppUserId = model.AppUserId,
            };

            await _context.AddAsync(varlik);
            await _context.SaveChangesAsync();

            return View();
        }


        public IActionResult List(int Id)
        {
            var result = _context.Users.Join(_context.Varliks, x => x.Id, y => y.AppUserId, (x, y)
                => new { Users = x, Varliks = y }).ToList();

            var varlikList = result.Select(x => new ListOfVarliksViewModel()
            {
                Id = x.Varliks.Id,
                VarlikName = x.Varliks.VarlikName,
                VarlikCode = x.Varliks.VarlikCode,
                VarlikDescription = x.Varliks.VarlikDescription,
                AppUserId = x.Varliks.AppUserId,
                AppUserName=x.Users.NameSurName


            }).OrderByDescending(x => x.Id).ToList();
            return View(varlikList);

        }
        public IActionResult UserVarlikList(int Id)
        {
            var result = _context.Users.Join(_context.Varliks, x => x.Id, y => y.AppUserId, (x, y)
                => new { Users = x, Varliks = y }).Where(x=> x.Users.Id==Id).ToList();

            var varlikList = result.Select(x => new ListOfVarliksViewModel()
            {
                Id = x.Varliks.Id,
                VarlikName = x.Varliks.VarlikName,
                VarlikCode = x.Varliks.VarlikCode,
                VarlikDescription = x.Varliks.VarlikDescription,               

            }).ToList();
            return View(varlikList);

        }






    }
}
