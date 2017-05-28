using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReviewCounter.Models;
using Microsoft.AspNetCore.Http;

namespace ReviewCounter.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ReviewCountingContext _context;

        public ReviewsController(ReviewCountingContext context)
        {
            _context = context;    
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            return View(await _context.Review
                .Include(r=> r.Project)
                .Include(r=> r.Version)
                .Include(r=> r.Output)
                .Include(r=> r.Author)
                .ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .SingleOrDefaultAsync(m => m.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            ViewData["Members"] = _context.Member.ToList();
            ViewData["Projects"] = _context.Project.ToList();
            ViewData["Outputs"] = _context.Output.ToList();
            ViewData["Versions"] = _context.Version.ToList();

            return View();
        }

        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // ReviewÇ™Ç†ÇÈÇ©ämîF
                var project = _context.Project.Single(p => p.ProjectId == int.Parse(collection["project"]));
                var version = _context.Version.Single(v => v.ReleaseId == int.Parse(collection["version"]));
                var output = _context.Output.Single(o => o.OutputId == int.Parse(collection["output"]));
                var review = _context.Review.Where(r => r.Project == project && r.Version == version && r.Output == output).ToArray();
                if (review.Count() > 0)
                {
                    ViewData["message"] = "Ç∑Ç≈Ç…ReviewÇ™ë∂ç›ÇµÇ‹Ç∑ÅB";
                    ViewData["Members"] = _context.Member.ToList();
                    ViewData["Projects"] = _context.Project.ToList();
                    ViewData["Outputs"] = _context.Output.ToList();
                    ViewData["Versions"] = _context.Version.ToList();
                    return View();
                }

                var ticket = int.Parse(collection["ticket"]);
                var author = _context.Member.SingleOrDefault(m => m.MemberId == int.Parse(collection["author"]));

                // Ç»Ç¢ÇÃÇ≈çÏê¨
                _context.Review.Add(new Review
                    {
                        Project = project,
                        Version = version,
                        Ticket = ticket,
                        Output = output,
                        Author = author
                    });
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewData["message"] = "";
                return View();
            }
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review.SingleOrDefaultAsync(m => m.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReviewId,Ticket")] Review review)
        {
            if (id != review.ReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.ReviewId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(review);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .SingleOrDefaultAsync(m => m.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Review.SingleOrDefaultAsync(m => m.ReviewId == id);
            _context.Review.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ReviewExists(int id)
        {
            return _context.Review.Any(e => e.ReviewId == id);
        }
    }
}
