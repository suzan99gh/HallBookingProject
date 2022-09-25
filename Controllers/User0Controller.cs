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
    public class User0Controller : Controller
    {
        private readonly ModelContext _context;

        public User0Controller(ModelContext context)
        {
            _context = context;
        }

        // GET: User0
        public async Task<IActionResult> Index()
        {

            int? id = HttpContext.Session.GetInt32("CustmerId");
            var custmUser = _context.User0s.Where(x => x.IdUser == id).FirstOrDefault();
            //----------------------
            ViewBag.Users = _context.User0s.ToList().Count();

            //------------------
            var modelContext = _context.User0s.Include(u => u.Role);
            return View(await modelContext.ToListAsync());
        }

        // GET: User0/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user0 = await _context.User0s
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.IdUser == id);
            if (user0 == null)
            {
                return NotFound();
            }

            return View(user0);
        }

        // GET: User0/Create
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.Userroles, "Roleid", "RoleName");
            return View();
        }

        // POST: User0/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUser,Fname,Lname,Email,Phonenum,ProfilePic,Roleid")] User0 user0)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user0);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roleid"] = new SelectList(_context.Userroles, "Roleid", "RoleName", user0.Roleid);
            return View(user0);
        }

        // GET: User0/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user0 = await _context.User0s.FindAsync(id);
            if (user0 == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.Userroles, "Roleid", "RoleName", user0.Roleid);
            return View(user0);
        }

        // POST: User0/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdUser,Fname,Lname,Email,Phonenum,ProfilePic,Roleid")] User0 user0)
        {
            if (id != user0.IdUser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user0);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!User0Exists(user0.IdUser))
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
            ViewData["Roleid"] = new SelectList(_context.Userroles, "Roleid", "RoleName", user0.Roleid);
            return View(user0);
        }

        // GET: User0/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user0 = await _context.User0s
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.IdUser == id);
            if (user0 == null)
            {
                return NotFound();
            }

            return View(user0);
        }

        // POST: User0/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var user0 = await _context.User0s.FindAsync(id);
            _context.User0s.Remove(user0);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool User0Exists(decimal id)
        {
            return _context.User0s.Any(e => e.IdUser == id);
        }

     

    }
}
