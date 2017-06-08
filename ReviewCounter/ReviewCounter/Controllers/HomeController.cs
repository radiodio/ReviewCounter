using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReviewCounter.Models;
using Microsoft.EntityFrameworkCore;

namespace ReviewCounter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ReviewCountingContext _context;

        public HomeController(ReviewCountingContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ReviewTime
                                .Include(rt => rt.Review)
                                .Include(rt => rt.Review.Project)
                                .Include(rt => rt.Review.Version)
                                .Include(rt => rt.Review.Output)
                                .Include(rt => rt.Review.Author)
                                .Include(rt => rt.Member)
                                .OrderBy(rt => rt.Review.Project)
                                .ThenBy(rt => rt.Review.Version)
                                .ThenBy(rt => rt.Review.Output)
                                .ThenBy(rt => rt.Review.Author)
                                .ThenBy(rt => rt.Member)
                                .ThenBy(rt => rt.Date)
                                .ToListAsync());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
