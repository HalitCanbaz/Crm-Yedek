using CrmApp.Models.Entities;
using CrmApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CrmApp.ViewModel.WorkViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrmApp.ViewModel.DutyViewModels;

namespace CrmApp.Controllers
{
    public class DutyController : Controller
    {

        private readonly SignInManager<AppUser> _SignInManager;
        private readonly UserManager<AppUser> _UserManager;
        private readonly CrmAppDbContext _context;
        public DutyController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, CrmAppDbContext context)
        {
            _SignInManager = signInManager;
            _UserManager = userManager;
            _context = context;
        }


        public IActionResult CreateDuty()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "NameSurName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDuty(CreateDutyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["message"] = "Bir hata oluştu";
                return View();
            }


            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "NameSurName");

            DateTime systemClock = DateTime.Now;
            DateTime controlClock = systemClock.AddMinutes(30);

            if (controlClock >= model.DeadLine)
            {
                TempData["timeMessage"] = "Talep ettiğiniz tarih sistem saatinden minimum 30 dk yukarıda olmak zorundadır!";
                return View();
            }

            string numberUp = "";

            DateTime thisYear = DateTime.Now;
            string years = Convert.ToString(thisYear.Year % 100);

            var currentDuty = await _context.Duty.OrderByDescending(x => x.TaskOrderNumber).FirstOrDefaultAsync();

            if (currentDuty != null)
            {
                string number = currentDuty.TaskOrderNumber.Substring(currentDuty.TaskOrderNumber.Length - 3);
                int numberIntl = Convert.ToInt32(number) + 1;
                string numberString = Convert.ToString(numberIntl);

                if (numberString.Length <= 1)
                {
                    numberUp = years + "-" + "000" + (Convert.ToString(numberString));

                }
                else if (numberString.Length <= 2)
                {
                    numberUp = years + "-" + "00" + (Convert.ToString(numberString));

                }
                else if (numberString.Length <= 3)
                {
                    numberUp = years + "-" + "0" + (Convert.ToString(numberString));

                }

            }
            else
            {
                numberUp = years + "-" + "0001";
            }

            var user = await _UserManager.FindByNameAsync(User.Identity!.Name!);

            var result = new Duty()
            {
                Title = model.Title,
                Description = model.Description,
                Status = model.Status,
                Progress = model.Progress,
                WhoIsCreate = user.NameSurName,
                Create = model.Create,
                Update = model.Update,
                DeadLine = model.DeadLine,
                Finished = model.Finished,
                AppUserId = model.AppUserId,
                CategoriesId = model.CategoriesId,
                Departman = user.DepartmanId,
                TaskOpenDepartman = user.DepartmanId,
                TaskOrderNumber = numberUp

            };

            await _context.AddAsync(result);
            await _context.SaveChangesAsync();
            TempData["message"] = "Görev ataması başarılı. Yeni kayıt açabilirsiniz!";

            return RedirectToAction(nameof(DutyController.CreateDuty));

        }

        //Listeleme İşlemleri YApıldı

        public async Task<IActionResult> DutyApprovedList(int Id)
        {
            var dutyList = await _context.Duty.ToListAsync();

            var userControl = await _UserManager.FindByNameAsync(User.Identity!.Name!);


            var worksListViewModel = dutyList.Where(x => x.Departman == (userControl.DepartmanId) & x.Status == "beklemede").Select(x => new DutyApprovedListViewModel()
            {
                Id = x.Id,
                Title = x.Title,
                Create = x.Create,
                DeadLine = x.DeadLine,
                WhoIsCreate = x.WhoIsCreate,
                Status = x.Status,
                DutyOrderNumber = x.TaskOrderNumber

            }).OrderByDescending(x => x.Id).ToList();
            return View(worksListViewModel);
        }







    }
}
