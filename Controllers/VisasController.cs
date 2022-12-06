using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HallBookingProject.Models;
using Microsoft.AspNetCore.Http;

namespace HallBookingProject.Controllers
{
    public class VisasController : Controller
    {
        private readonly ModelContext _context;

        public VisasController(ModelContext context)
        {
            _context = context;
        }

        // GET: Visas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Visas.ToListAsync());
        }

        // GET: Visas/Details/5
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

        // GET: Visas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Visas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*decimal id , */ [Bind("IdPayment,CardName,CardNumber,Cvc,ExprDate,Balance")] Visa visa)
        {
            if (ModelState.IsValid)
            {
                //visa.Hallid = id;

                _context.Add(visa);
                await _context.SaveChangesAsync();
                return RedirectToAction( "Index" , "Home");
            }
            return View(visa);
        }

        // GET: Visas/Edit/5
        public async Task<IActionResult> Edit(decimal? id , Hall hall )
        {
            ViewBag.price = HttpContext.Session.GetInt32("HallPrice");
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

        // POST: Visas/Edit/5
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

        // GET: Visas/Delete/5
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

        // POST: Visas/Delete/5
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
