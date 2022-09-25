using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HallBookingProject.Models;

namespace HallBookingProject.Controllers
{
    public class CategoryiesController : Controller
    {
        private readonly ModelContext _context;

        public CategoryiesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Categoryies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categoryys.ToListAsync());
        }

        // GET: Categoryies/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryy = await _context.Categoryys
                .FirstOrDefaultAsync(m => m.IdCat == id);
            if (categoryy == null)
            {
                return NotFound();
            }

            return View(categoryy);
        }

        // GET: Categoryies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoryies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCat,CatName")] Categoryy categoryy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryy);
        }

        // GET: Categoryies/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryy = await _context.Categoryys.FindAsync(id);
            if (categoryy == null)
            {
                return NotFound();
            }
            return View(categoryy);
        }

        // POST: Categoryies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdCat,CatName")] Categoryy categoryy)
        {
            if (id != categoryy.IdCat)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryyExists(categoryy.IdCat))
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
            return View(categoryy);
        }

        // GET: Categoryies/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryy = await _context.Categoryys
                .FirstOrDefaultAsync(m => m.IdCat == id);
            if (categoryy == null)
            {
                return NotFound();
            }

            return View(categoryy);
        }

        // POST: Categoryies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var categoryy = await _context.Categoryys.FindAsync(id);
            _context.Categoryys.Remove(categoryy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryyExists(decimal id)
        {
            return _context.Categoryys.Any(e => e.IdCat == id);
        }
    }
}
