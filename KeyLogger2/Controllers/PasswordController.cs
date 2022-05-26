using Microsoft.AspNetCore.Mvc;
using KeyLogger2.Data;
using KeyLogger2.Models;
using Microsoft.EntityFrameworkCore;

namespace KeyLogger2.Controllers
{
    public class PasswordController : Controller
    {
        private readonly KeyLogContext _context;

        public PasswordController(KeyLogContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? id)
        {
            // numPass is the amount of passwords shown per page
            const int numPass = 5;
            const int pageOffSet = 1;
            int currPage = id ?? 1;

            int totalNumOfPasswords = await _context.Password.CountAsync();
            double maxNumPages = Math.Ceiling((double)totalNumOfPasswords / numPass);
            int lastPage = Convert.ToInt32(maxNumPages);

            
            List<Passwords> passwords = await (from Passwords in _context.Password
                                               from Members in _context.Member
                                               where Passwords.MemberId == Members.MemberId
                                               select Passwords
                                               )
                                               .Skip(numPass * (currPage - pageOffSet))
                                               .Take(numPass)
                                               .ToListAsync();

            

            PasswordPageViewModel catModel = new(passwords, lastPage, currPage);
            return View(catModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Passwords passwords)
        {
            if (ModelState.IsValid)
            {
                _context.Password.Add(passwords);
                await _context.SaveChangesAsync();

                ViewData["Message"] = $"{passwords.UserName} was added correctly";
                return View();
            }
            return View(passwords);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Passwords? passwordChange = await _context.Password.FindAsync(id);
            if (passwordChange == null)
            {
                return NotFound();
            }
            return View(passwordChange);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Passwords passwordModel)
        {
            if (ModelState.IsValid)
            {
                _context.Password.Update(passwordModel);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{passwordModel.UserName} was updated successfully";
                return RedirectToAction("Index");
            }
            return View(passwordModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Passwords passwordDeletion = await _context.Password.FindAsync(id);
            if(passwordDeletion == null)
            {
                return NotFound();
            }
            return View(passwordDeletion);
        }

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteCon(int id)
        {
            Passwords passwordDeletion = await _context.Password.FindAsync(id);
            if (passwordDeletion != null)
            {
                _context.Password.Remove(passwordDeletion);
                await _context.SaveChangesAsync();
                TempData["Message"] = "The account was deleted from records";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "The account has been removed already";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var passDetails = await _context.Password.FindAsync(id);

            if (passDetails == null)
            {
                return NotFound();
            }
            return RedirectToAction("ConfirmPass", "Members");
        }
    }
}