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
    public class FeedbacksController : Controller
    {
        private readonly ModelContext _context;

        public FeedbacksController(ModelContext context)
        {
            _context = context;
        }

        // GET: Feedbacks
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Feedbacks.Include(f => f.Feed);
            return View(await modelContext.ToListAsync());
        }

        // GET: Feedbacks/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks
                .Include(f => f.Feed)
                .FirstOrDefaultAsync(m => m.IdFeed == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // GET: Feedbacks/Create
        public IActionResult Create()
        {
            ViewData["Feedid"] = new SelectList(_context.User0s, "IdUser", "Email");
            return View();
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(decimal id, [Bind("IdFeed,FullName,Email,Feedback1,Feedid")] Feedback feedback , int iduser)
        {
            if (ModelState.IsValid)
            {
               feedback.Email= "Requested";
                feedback.Feedid = HttpContext.Session.GetInt32("CustmerId");
                HttpContext.Session.GetString("CustmerEmail" );
         
                _context.Add(feedback);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");

            }
            ViewData["Feedid"] = new SelectList(_context.User0s, "IdUser", "Email", feedback.Feedid);
            return View(feedback);
        }

        // GET: Feedbacks/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
           

            
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            ViewData["Feedid"] = new SelectList(_context.User0s, "IdUser", "Email", feedback.Feedid);
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdFeed,FullName,Email,Feedback1,Feedid")] Feedback feedback)
        {
            //var modelContext = _context.Feedbacks.Where(x => x.Feedback).ToList();
            if (id != feedback.IdFeed)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    //if (feedback.Email != null && feedback.Feedback1 == null)
                    //{
                      

                 
                    //    _context.Update(feedback.Email);
                    //    await _context.SaveChangesAsync();

                    //}



                    _context.Update(feedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbackExists(feedback.IdFeed))
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
            ViewData["Feedid"] = new SelectList(_context.User0s, "IdUser", "Email", feedback.Feedid);
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks
                .Include(f => f.Feed)
                .FirstOrDefaultAsync(m => m.IdFeed == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedbackExists(decimal id)
        {
            return _context.Feedbacks.Any(e => e.IdFeed == id);
        }
    }
}
