using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HallBookingProject.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace HallBookingProject.Controllers
{
    public class HomesController : Controller
    {
        private readonly ModelContext _context;
        public readonly IWebHostEnvironment _webHostEnviroment;
        public HomesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnviroment = webHostEnvironment;
        }

        // GET: Homes
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Homes.Include(h => h.About).Include(h => h.Contact);
            return View(await modelContext.ToListAsync());
        }

        // GET: Homes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _context.Homes
                .Include(h => h.About)
                .Include(h => h.Contact)
                .FirstOrDefaultAsync(m => m.IdHome == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        // GET: Homes/Create
        public IActionResult Create()
        {
            ViewData["Aboutid"] = new SelectList(_context.AboutUs, "IdAbout", "Description");
            ViewData["Contactid"] = new SelectList(_context.Contacts, "IdContact", "Email");
            return View();
        }

        // POST: Homes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHome,Img1,Img2,Img3,Aboutid,Contactid,Homeimg")] Home home)
        {
            if (ModelState.IsValid)
            {

                //if (home.Homeimg != null)
                //{
                //    //------------------------
                //    string wwwRootPath = _webHostEnvironment.WebRootPath;
                //    string fileName = Guid.NewGuid().ToString() + "_" + home.Homeimg.FileName;
                //    string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                //    using (var fileStream = new FileStream(path, FileMode.Create))
                //    {
                //        await home.Homeimg.CopyToAsync(fileStream);
                //    }

                //    home.Img1 = fileName;
                //}





                _context.Add(home);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Aboutid"] = new SelectList(_context.AboutUs, "IdAbout", "Description", home.Aboutid);
            ViewData["Contactid"] = new SelectList(_context.Contacts, "IdContact", "Email", home.Contactid);
            return View(home);
        }

        // GET: Homes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _context.Homes.FindAsync(id);
            if (home == null)
            {
                return NotFound();
            }
            ViewData["Aboutid"] = new SelectList(_context.AboutUs, "IdAbout", "Description", home.Aboutid);
            ViewData["Contactid"] = new SelectList(_context.Contacts, "IdContact", "Email", home.Contactid);
            return View(home);
        }

        // POST: Homes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdHome,Img1,Img2,Img3,Aboutid,Contactid,Homeimg")] Home home)
        {
            if (id != home.IdHome)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                   


                try
                {

                    if (home.Homeimg != null)
                    {
                        string wwwrootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "" + home.Homeimg.FileName;
                        string path = Path.Combine(wwwrootPath + "/Image/", fileName);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await home.Homeimg.CopyToAsync(filestream);
                        }
                        home.Img1 = fileName;
                    }


                    _context.Update(home);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeExists(home.IdHome))
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


            ViewData["Aboutid"] = new SelectList(_context.AboutUs, "IdAbout", "Description", home.Aboutid);
            ViewData["Contactid"] = new SelectList(_context.Contacts, "IdContact", "Email", home.Contactid);
            return View(home);
        }

        // GET: Homes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _context.Homes
                .Include(h => h.About)
                .Include(h => h.Contact)
                .FirstOrDefaultAsync(m => m.IdHome == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        // POST: Homes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var home = await _context.Homes.FindAsync(id);
            _context.Homes.Remove(home);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeExists(decimal id)
        {
            return _context.Homes.Any(e => e.IdHome == id);
        }
    }
}
