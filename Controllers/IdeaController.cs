using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnterpriseWeb.Models;

namespace EnterpriseWeb.Controllers
{
    public class IdeaController : Controller
    {
        private readonly EnterpriseWebContext _context;

        public IdeaController(EnterpriseWebContext context)
        {
            _context = context;
        }

        // GET: Idea
        public async Task<IActionResult> Index()
        {
            var enterpriseWebContext = _context.Idea.Include(i => i.ClosureDate).Include(i => i.Department).Include(i => i.User);
            return View(await enterpriseWebContext.ToListAsync());
        }

        // GET: Idea/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idea = await _context.Idea
                .Include(i => i.ClosureDate)
                .Include(i => i.Department)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (idea == null)
            {
                return NotFound();
            }

            return View(idea);
        }

        // GET: Idea/Create
        public IActionResult Create()
        {
            ViewData["ClosureDateID"] = new SelectList(_context.Set<ClosureDate>(), "Id", "Id");
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "Id", "Id");
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: Idea/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,UserID,SubmissionDate,SupportingDocuments,DepartmentID,ClosureDateID")] Idea idea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(idea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClosureDateID"] = new SelectList(_context.Set<ClosureDate>(), "Id", "Id", idea.ClosureDateID);
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "Id", "Id", idea.DepartmentID);
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id", idea.UserID);
            return View(idea);
        }

        // GET: Idea/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idea = await _context.Idea.FindAsync(id);
            if (idea == null)
            {
                return NotFound();
            }
            ViewData["ClosureDateID"] = new SelectList(_context.Set<ClosureDate>(), "Id", "Id", idea.ClosureDateID);
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "Id", "Id", idea.DepartmentID);
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id", idea.UserID);
            return View(idea);
        }

        // POST: Idea/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,UserID,SubmissionDate,SupportingDocuments,DepartmentID,ClosureDateID")] Idea idea)
        {
            if (id != idea.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(idea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdeaExists(idea.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClosureDateID"] = new SelectList(_context.Set<ClosureDate>(), "Id", "Id", idea.ClosureDateID);
            ViewData["DepartmentID"] = new SelectList(_context.Set<Department>(), "Id", "Id", idea.DepartmentID);
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id", idea.UserID);
            return View(idea);
        }

        // GET: Idea/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idea = await _context.Idea
                .Include(i => i.ClosureDate)
                .Include(i => i.Department)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (idea == null)
            {
                return NotFound();
            }

            return View(idea);
        }

        // POST: Idea/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var idea = await _context.Idea.FindAsync(id);
            _context.Idea.Remove(idea);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IdeaExists(int id)
        {
            return _context.Idea.Any(e => e.Id == id);
        }
    }
}