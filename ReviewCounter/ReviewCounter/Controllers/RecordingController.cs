using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReviewCounter.Models;

namespace ReviewCounter.Controllers
{
    public class RecordingController : Controller
    {
        private readonly ReviewCountingContext _context;

        public RecordingController(ReviewCountingContext context)
        {
            _context = context;
        }

        // GET: Recording
        public ActionResult Index()
        {
            ViewData["Members"] = _context.Member.ToList();
            ViewData["Projects"] = _context.Project.ToList();
            ViewData["Outputs"] = _context.Output.ToList();
            ViewData["Versions"] = _context.Version.ToList();

            return View();
        }

        // GET: Recording/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Recording/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recording/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // ReviewÇ™Ç†ÇÈÇ©ämîF
                var project = _context.Project.Single(p => p.ProjectId == int.Parse(collection["project"]));
                var version = _context.Version.Single(p => p.ReleaseId == int.Parse(collection["version"]));
                var output = _context.Output.Single(p => p.OutputId == int.Parse(collection["output"]));

                // Ç»ÇØÇÍÇŒçÏÇÈ

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return View();
            }
        }

        // GET: Recording/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Recording/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Recording/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Recording/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}