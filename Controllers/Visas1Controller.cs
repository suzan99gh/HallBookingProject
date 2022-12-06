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
    public class Visas1Controller : Controller
    {
        private readonly ModelContext _context;

        public Visas1Controller(ModelContext context)
        {
            _context = context;
        }

        // GET: Visas1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Visas.ToListAsync());
        }

        // GET: Visas1/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visa = await _context.Visas
                .FirstOrDefaultAsync(m => m.IdPayment == id);
            if (visa == null)
            {
                return NotFound();
            }

            return View(visa);
        }

        // GET: Visas1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Visas1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPayment,CardName,CardNumber,Cvc,ExprDate,Balance")] Visa visa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(visa);
        }

        // GET: Visas1/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visa = await _context.Visas.FindAsync(id);
            if (visa == null)
            {
                return NotFound();
            }
            return View(visa);
        }

        // POST: Visas1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdPayment,CardName,CardNumber,Cvc,ExprDate,Balance")] Visa visa)
        {
            if (id != visa.IdPayment)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisaExists(visa.IdPayment))
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
            return View(visa);
        }

        // GET: Visas1/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visa = await _context.Visas
                .FirstOrDefaultAsync(m => m.IdPayment == id);
            if (visa == null)
            {
                return NotFound();
            }

            return View(visa);
        }

        // POST: Visas1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var visa = await _context.Visas.FindAsync(id);
            _context.Visas.Remove(visa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisaExists(decimal id)
        {
            return _context.Visas.Any(e => e.IdPayment == id);
        }
    }
}
