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
    public class IdeaCategoryController : Controller
    {
        private readonly EnterpriseWebContext _context;

        public IdeaCategoryController(EnterpriseWebContext context)
        {
            _context = context;
        }

        // GET: IdeaCategory
        public async Task<IActionResult> Index()
        {
            var enterpriseWebContext = _context.IdeaCategory.Include(i => i.Category).Include(i => i.Idea);
            return View(await enterpriseWebContext.ToListAsync());
        }

        // GET: IdeaCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ideaCategory = await _context.IdeaCategory
                .Include(i => i.Category)
                .Include(i => i.Idea)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ideaCategory == null)
            {
                return NotFound();
            }

            return View(ideaCategory);
        }

        // GET: IdeaCategory/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "Id", "Id");
            ViewData["IdeaID"] = new SelectList(_context.Idea, "Id", "Id");
            return View();
        }

        // POST: IdeaCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdeaID,CategoryID")] IdeaCategory ideaCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ideaCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "Id", "Id", ideaCategory.CategoryID);
            ViewData["IdeaID"] = new SelectList(_context.Idea, "Id", "Id", ideaCategory.IdeaID);
            return View(ideaCategory);
        }

        // GET: IdeaCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ideaCategory = await _context.IdeaCategory.FindAsync(id);
            if (ideaCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "Id", "Id", ideaCategory.CategoryID);
            ViewData["IdeaID"] = new SelectList(_context.Idea, "Id", "Id", ideaCategory.IdeaID);
            return View(ideaCategory);
        }

        // POST: IdeaCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdeaID,CategoryID")] IdeaCategory ideaCategory)
        {
            if (id != ideaCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ideaCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdeaCategoryExists(ideaCategory.Id))
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
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "Id", "Id", ideaCategory.CategoryID);
            ViewData["IdeaID"] = new SelectList(_context.Idea, "Id", "Id", ideaCategory.IdeaID);
            return View(ideaCategory);
        }

        // GET: IdeaCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ideaCategory = await _context.IdeaCategory
                .Include(i => i.Category)
                .Include(i => i.Idea)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ideaCategory == null)
            {
                return NotFound();
            }

            return View(ideaCategory);
        }

        // POST: IdeaCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ideaCategory = await _context.IdeaCategory.FindAsync(id);
            _context.IdeaCategory.Remove(ideaCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IdeaCategoryExists(int id)
        {
            return _context.IdeaCategory.Any(e => e.Id == id);
        }
    }
}