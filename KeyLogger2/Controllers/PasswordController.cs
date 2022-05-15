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
                                               where Members.MemberId == Passwords.MemberId
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

    }
}
