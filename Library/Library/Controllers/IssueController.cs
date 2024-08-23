using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Library.Models;

namespace Library.Controllers
{
    public class IssueController : Controller
    {
        private readonly LibraryContext _context;

        public IssueController(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var issues = _context.Issue.ToList();
            return View(issues);
        }
        // POST: Issue/Create
        [HttpPost]
        public IActionResult Create([Bind("IssueID,Ititle,Iauthor,IssuePeriod,Due")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issue);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(issue);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = _context.Issue.Find(id);
            if (issue == null)
            {
                return NotFound();
            }
            return View(issue);
        }
        [HttpPost]
        public IActionResult Edit(int id, [Bind("IssueID,Ititle,Iauthor,IssuePeriod,Due")] Issue issue)
        {
            if (id != issue.IssueID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(issue);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(issue);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = _context.Issue.FirstOrDefault(m => m.IssueID == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var issue = _context.Issue.Find(id);
            _context.Issue.Remove(issue);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
