using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReviewCounter.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.IO;

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
                                .Where(rt => rt.Review.Version.Closed == false)
                                .OrderBy(rt => rt.Review.Project)
                                .ThenBy(rt => rt.Review.Version)
                                .ThenBy(rt => rt.Review.Output)
                                .ThenBy(rt => rt.Review.Author)
                                .ThenBy(rt => rt.Member)
                                .ThenBy(rt => rt.Date)
                                .ToListAsync());
        }

        public IActionResult Amount(DateTime? month, int? projectId)
        {
            if (month == null || projectId == null)
            {
                ViewBag.Projects = _context.Project.ToList();
                return View("Monthly");
            }
            ViewBag.Month = (DateTime)month;
            ViewBag.ProjectId = (int)projectId;
            var from = (DateTime)month;
            var to = from.AddMonths(1).AddDays(-1.0);
            var list = _context.ReviewTime.Include(rt => rt.Review)
                                          .Include(rt => rt.Review.Project)
                                          .Include(rt => rt.Review.Version)
                                          .Include(rt => rt.Review.Output)
                                          .Include(rt => rt.Review.Author)
                                          .Include(rt => rt.Member)
                                          .Where(rt => rt.Review.Version.Closed == false)
                                          .Where(rt => rt.Review.Project.ProjectId == (int)projectId)
                                          .OrderBy(rt => rt.Review.Project)
                                          .ThenBy(rt => rt.Review.Version)
                                          .ThenBy(rt => rt.Review.Output)
                                          .ThenBy(rt => rt.Review.Author)
                                          .ThenBy(rt => rt.Member)
                                          .ThenBy(rt => rt.Date)
                                          .Where(rt => rt.Date >= from)
                                          .Where(rt => rt.Date <= to)
                                          .ToList();
            return View(list);
        }

        public IActionResult Error()
        {
            return View();
        }

        public FileResult DownloadAmount(DateTime? month, int? projectId)
        {
            if (month == null || projectId == null) { throw new FileNotFoundException(); }
            var from = (DateTime)month;
            var to = from.AddMonths(1).AddDays(-1.0);
            var list = CsvRecord.ConvertToCsvRecord
                       (_context.ReviewTime
                                .Include(rt => rt.Review)
                                .Include(rt => rt.Review.Project)
                                .Include(rt => rt.Review.Version)
                                .Include(rt => rt.Review.Output)
                                .Include(rt => rt.Review.Author)
                                .Include(rt => rt.Member)
                                .Where(rt => rt.Review.Version.Closed == false)
                                .Where(rt => rt.Review.Project.ProjectId == (int)projectId)
                                .OrderBy(rt => rt.Review.Project)
                                .ThenBy(rt => rt.Review.Version)
                                .ThenBy(rt => rt.Review.Output)
                                .ThenBy(rt => rt.Review.Author)
                                .ThenBy(rt => rt.Member)
                                .ThenBy(rt => rt.Date)
                                .Where(rt => rt.Date >= from)
                                .Where(rt => rt.Date <= to)
                                .ToList()
                       );
            return Download(list);
        }

        public FileResult DownloadIndex()
        {
            var list = CsvRecord.ConvertToCsvRecord
                       (_context.ReviewTime
                                   .Include(rt => rt.Review)
                                   .Include(rt => rt.Review.Project)
                                   .Include(rt => rt.Review.Version)
                                   .Include(rt => rt.Review.Output)
                                   .Include(rt => rt.Review.Author)
                                   .Include(rt => rt.Member)
                                   .Where(rt => rt.Review.Version.Closed == false)
                                   .OrderBy(rt => rt.Review.Project)
                                   .ThenBy(rt => rt.Review.Version)
                                   .ThenBy(rt => rt.Review.Output)
                                   .ThenBy(rt => rt.Review.Author)
                                   .ThenBy(rt => rt.Member)
                                   .ThenBy(rt => rt.Date)
                                   .ToList()
                       );
            return Download(list);
        }

        private FileResult Download(List<CsvRecord> list)
        {
            string path = "review.csv";
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read, 4096, FileOptions.SequentialScan))
            using (var streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8, 1024, false))
            {
                streamWriter.WriteLine("Project,Version,Backlog,ProcessOutput,Reviewee,Reviewer,Date,Time");

                StringBuilder sb = new StringBuilder();
                foreach (var item in list)
                {
                    sb.Append(item.Project).Append(",")
                        .Append(item.Version).Append(",")
                        .Append(item.Backlog).Append(",")
                        .Append(item.ProcessOutput).Append(",")
                        .Append(item.Reviewee).Append(",")
                        .Append(item.Reviewer).Append(",")
                        .Append(item.Date).Append(",").Append(item.Time);
                    streamWriter.WriteLine(sb.ToString());
                    sb.Clear();
                }
                sb.Append(",")
                    .Append(",")
                    .Append(",")
                    .Append(",")
                    .Append(",")
                    .Append(",").Append("Sum(min)")
                    .Append(",").Append(list.Sum(x => x.Time));
                streamWriter.WriteLine(sb.ToString());
            }
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, "application/x-msdownload", path);
        }
    }
}
