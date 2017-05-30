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
    public class ReviewTimesController : Controller
    {
        private readonly ReviewCountingContext _context;

        public ReviewTimesController(ReviewCountingContext context)
        {
            _context = context;    
        }

        // GET: ReviewTimes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Review
                                .Include(r=> r.Project)
                                .Include(r => r.Version)
                                .Include(r => r.Output)
                                .Include(r => r.Author)
                                .ToListAsync());
        }

        // GET: ReviewTimes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviewTime = await _context.ReviewTime
                .SingleOrDefaultAsync(m => m.Id == id);
            if (reviewTime == null)
            {
                return NotFound();
            }

            return View(reviewTime);
        }

        // GET: ReviewTimes/Create
        public IActionResult Create(int? id)
        {
            ViewBag.ReviewId = id;
            ViewBag.Members = _context.Member.ToList();
            return View();
        }

        // POST: ReviewTimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            int reviewId;
            if (!int.TryParse(collection["reviewId"], out reviewId))
            {
                return RedirectToAction("Index");
            }

            try
            {
                var review = _context.Review.SingleOrDefault(r => r.ReviewId == int.Parse(collection["reviewId"]));
                var member = _context.Member.SingleOrDefault(m => m.MemberId == int.Parse(collection["member"]));
                var dateTime = DateTime.Parse(collection["date"]);
                var time = int.Parse(collection["time"]);
                _context.ReviewTime.Add(new ReviewTime
                {
                    Review = review,
                    Member = member,
                    Date = dateTime,
                    Time = time
                });
                _context.SaveChanges();

                TempData["message"] = "éûä‘í«â¡Ç…ê¨å˜";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["message"] = "éûä‘í«â¡Ç…é∏îs";
                return RedirectToAction("Index");
            }
        }

        // GET: ReviewTimes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviewTime = await _context.ReviewTime.SingleOrDefaultAsync(m => m.Id == id);
            if (reviewTime == null)
            {
                return NotFound();
            }
            return View(reviewTime);
        }

        // POST: ReviewTimes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Time")] ReviewTime reviewTime)
        {
            if (id != reviewTime.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reviewTime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewTimeExists(reviewTime.Id))
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
            return View(reviewTime);
        }

        // GET: ReviewTimes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviewTime = await _context.ReviewTime
                .SingleOrDefaultAsync(m => m.Id == id);
            if (reviewTime == null)
            {
                return NotFound();
            }

            return View(reviewTime);
        }

        // POST: ReviewTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reviewTime = await _context.ReviewTime.SingleOrDefaultAsync(m => m.Id == id);
            _context.ReviewTime.Remove(reviewTime);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ReviewTimeExists(int id)
        {
            return _context.ReviewTime.Any(e => e.Id == id);
        }
    }
}
