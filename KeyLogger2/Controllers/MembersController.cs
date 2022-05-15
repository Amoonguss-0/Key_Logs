using KeyLogger2.Data;
using KeyLogger2.Models;
using Microsoft.AspNetCore.Mvc;

namespace KeyLogger2.Controllers
{
    public class MembersController : Controller
    {
        private readonly KeyLogContext _context;

        public MembersController(KeyLogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regModel)
        {
            if (ModelState.IsValid)
            {
                // map registerViewModel data to member object
                Members newMember = new()
                {
                    Email = regModel.Email,
                    Password = regModel.Password
                };

                _context.Member.Add(newMember);
                await _context.SaveChangesAsync();
                LogUserIn(newMember.Email);
                return RedirectToAction("Index", "Home");
            }
            return View(regModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                // Check DB for credentials 
                Members? m = (from member in _context.Member
                             where member.Email == loginModel.Email
                             && member.Password == loginModel.Password
                             select member).SingleOrDefault();
                // If exists, send to homepage
                if (m != null)
                {
                    LogUserIn(loginModel.Email);
                    return RedirectToAction("Index", "Home");
                }
                // If no record matches, display error
                ModelState.AddModelError(String.Empty, "Credentials not found!");
            }
            // Return page if no record is found, or ModelState is invalid
            return View(loginModel);
        }

        private void LogUserIn(String email)
        {
            HttpContext.Session.SetString("Email", email);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
