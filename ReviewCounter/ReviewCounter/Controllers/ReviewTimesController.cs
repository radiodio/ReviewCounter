using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReviewCounter.Models;

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
            return View(await _context.ReviewTime.ToListAsync());
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReviewTimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Time")] ReviewTime reviewTime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reviewTime);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(reviewTime);
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
