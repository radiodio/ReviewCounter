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
    public class OutputsController : Controller
    {
        private readonly ReviewCountingContext _context;

        public OutputsController(ReviewCountingContext context)
        {
            _context = context;    
        }

        // GET: Outputs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Output.ToListAsync());
        }

        // GET: Outputs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var output = await _context.Output
                .SingleOrDefaultAsync(m => m.OutputId == id);
            if (output == null)
            {
                return NotFound();
            }

            return View(output);
        }

        // GET: Outputs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Outputs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OutputId,ProcessOutput")] Output output)
        {
            if (ModelState.IsValid)
            {
                _context.Add(output);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(output);
        }

        // GET: Outputs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var output = await _context.Output.SingleOrDefaultAsync(m => m.OutputId == id);
            if (output == null)
            {
                return NotFound();
            }
            return View(output);
        }

        // POST: Outputs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OutputId,ProcessOutput")] Output output)
        {
            if (id != output.OutputId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(output);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OutputExists(output.OutputId))
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
            return View(output);
        }

        // GET: Outputs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var output = await _context.Output
                .SingleOrDefaultAsync(m => m.OutputId == id);
            if (output == null)
            {
                return NotFound();
            }

            return View(output);
        }

        // POST: Outputs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var output = await _context.Output.SingleOrDefaultAsync(m => m.OutputId == id);
            _context.Output.Remove(output);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool OutputExists(int id)
        {
            return _context.Output.Any(e => e.OutputId == id);
        }
    }
}
