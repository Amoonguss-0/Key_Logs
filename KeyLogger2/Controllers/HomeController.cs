using KeyLogger2.Data;
using KeyLogger2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace KeyLogger2.Controllers
{
    public class HomeController : Controller
    {
        private readonly KeyLogContext _context;
        public HomeController(KeyLogContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? ForumId)
        {
            const int numPosts = 15;
            const int pageOffset = 1;
            int currPage = ForumId ?? 1;

            int totalNumberOFPosts = await _context.Homes.CountAsync();
            double maxNumPages = Math.Ceiling((double)totalNumberOFPosts / numPosts);
            int lastPage = Convert.ToInt32(maxNumPages);

            List<Home> homes = await (from Home in _context.Homes
                                      select Home)
                                      .Skip(numPosts * (currPage - pageOffset))
                                      .Take(numPosts)
                                      .ToListAsync();
            HomeViewModel catModel = new(homes, lastPage, currPage);
            return View(catModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}