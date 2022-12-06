using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HallBookingProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace HallBookingProject.Controllers
{
    public class User0Controller : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public User0Controller(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnviroment = webHostEnvironment;
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
        public async Task<IActionResult> Edit(decimal id, [Bind("IdUser,Fname,Lname,Email,Phonenum,ProfilePic,Roleid,ImageProfile")] User0 user0)
        {
            if (id != user0.IdUser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (user0.ImageProfile != null)
                {
                    string wwwrootPath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "" + user0.ImageProfile.FileName;
                    string path = Path.Combine(wwwrootPath + "/Image/", fileName);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await user0.ImageProfile.CopyToAsync(filestream);
                    }
                    user0.ProfilePic = fileName;
                }
                _context.Update(user0);
                await _context.SaveChangesAsync();
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


        public async Task<IActionResult> UserProfile ()
        {
            ViewBag.CustmerId = HttpContext.Session.GetInt32("CustmerId");
            ViewBag.CustmerEmail = HttpContext.Session.GetString("CustmerEmail");
            ViewBag.CustmerPic = HttpContext.Session.GetString("CustmerPic");

            int? id = HttpContext.Session.GetInt32("CustmerId");
            var user = _context.User0s.Where(x => x.IdUser == id).FirstOrDefault();
        
            return View(user);
        }

        public async Task<IActionResult> UserUpdate(decimal id, [Bind("IdUser,Fname,Lname,Email,Phonenum,ProfilePic,Roleid,ImageProfile")] User0 user0)
        {
            if (id != user0.IdUser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (user0.ImageProfile != null)
                {
                    string wwwrootPath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "" + user0.ImageProfile.FileName;
                    string path = Path.Combine(wwwrootPath + "/Image/", fileName);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await user0.ImageProfile.CopyToAsync(filestream);
                    }
                    user0.ProfilePic = fileName;
                }
                _context.Update(user0);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roleid"] = new SelectList(_context.Userroles, "Roleid", "RoleName", user0.Roleid);
            return View(user0);
        }
    }
}
