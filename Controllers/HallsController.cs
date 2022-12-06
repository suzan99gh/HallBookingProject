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
    public class HallsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HallsController(ModelContext context , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;

            _webHostEnvironment = webHostEnvironment;
        }
        //-------------------
        //-----------------
        //---------------------
        //----------------
        //--------------
        // GET: Halls
        public async Task<IActionResult> Index()
        {
            ViewBag.Halls = _context.Halls.ToList().Count();
            const string V = "ViewBag.Halls";
            HttpContext.Session.SetString("Hall_Count", V);

            //---------------------------
            var modelContext = _context.Halls.Include(h => h.Cat);
            return View(await modelContext.ToListAsync());
        }

        // GET: Halls/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hall = await _context.Halls
                .Include(h => h.Cat)
                .FirstOrDefaultAsync(m => m.IdHall == id);
            if (hall == null)
            {
                return NotFound();
            }

            return View(hall);
        }

        // GET: Halls/Create
        public IActionResult Create()
        {
            ViewData["Catid"] = new SelectList(_context.Categoryys, "IdCat", "CatName");
            return View();
        }

        // POST: Halls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHall,HallName,HallDesc,Img1,Img2,Img3,Catid,ImageFile")] Hall hall)
        {
            if (ModelState.IsValid)
            {
                if (hall.ImageFile != null)
                {
                    //------------------------
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + hall.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await hall.ImageFile.CopyToAsync(fileStream);
                    }

                    hall.Img1 = fileName;
                }

                _context.Add(hall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Catid"] = new SelectList(_context.Categoryys, "IdCat", "CatName", hall.Catid);
            return View(hall);
        }

        // GET: Halls/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hall = await _context.Halls.FindAsync(id);
            if (hall == null)
            {
                return NotFound();
            }
            ViewData["Catid"] = new SelectList(_context.Categoryys, "IdCat", "CatName", hall.Catid);
            return View(hall);
        }

        // POST: Halls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdHall,HallName,HallDesc,Img1,Img2,Img3,Catid,ImageFile")] Hall hall)
        {
            if (id != hall.IdHall)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (hall.ImageFile != null)
                {
                    //------------------------
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + hall.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await hall.ImageFile.CopyToAsync(fileStream);
                    }

                    hall.Img1 = fileName;
                   _context.Update(hall);
                  await _context.SaveChangesAsync();
                }
                

                try
                {

                 
                    _context.Update(hall);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HallExists(hall.IdHall))
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
            ViewData["Catid"] = new SelectList(_context.Categoryys, "IdCat", "CatName", hall.Catid);
            return View(hall);
        }

        // GET: Halls/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hall = await _context.Halls
                .Include(h => h.Cat)
                .FirstOrDefaultAsync(m => m.IdHall == id);
            if (hall == null)
            {
                return NotFound();
            }

            return View(hall);
        }

        // POST: Halls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var hall = await _context.Halls.FindAsync(id);
            _context.Halls.Remove(hall);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HallExists(decimal id)
        {
            return _context.Halls.Any(e => e.IdHall == id);
        }

        //public IActionResult UserIndex()
        //{
        //    return View();
        //}



        [HttpGet]
        public IActionResult UserIndex(int? Id)
        {
            ViewBag.CustmerId = HttpContext.Session.GetInt32("CustmerId");
            ViewBag.CustmerEmail = HttpContext.Session.GetString("CustmerEmail");
            ViewBag.CustmerPic = HttpContext.Session.GetString("CustmerPic");

            Hall hall = new Hall();
/*Where(x => x.Catid == Id)*/;
            var halls = _context.Halls.ToList();
            return View(halls);
        }


        [HttpPost]
        public async Task<IActionResult> UserIndex(int? Id, /*string searchBy,*/ string searchTerm, Hall hall)

        {
            var halls = _context.Halls.ToList();
            ViewBag.CustmerId = HttpContext.Session.GetInt32("CustmerId");
            ViewBag.CustmerEmail = HttpContext.Session.GetString("CustmerEmail");
            ViewBag.CustmerPic = HttpContext.Session.GetString("CustmerPic");
            HttpContext.Session.SetString("HallPrice", hall.Img2);
            //var modelContext = _context.Reservations.Include(x => x.Hall) ;

            try
            {
                if (string.IsNullOrEmpty(searchTerm))
                {
                    var modelContext = _context.Halls.Where(x => x.Catid == Id);          /*Include(h => h.Img3).Include(h => h.HallName)*/
                    //return View(await modelContext.ToListAsync());
                    return View(halls);
                }
                else
                {
                    var hallcat = _context.Halls.Where(x => x.Catid == Id);

                    var modelContext = _context.Halls.Where(x => x.HallName.ToUpper().Contains(searchTerm.ToUpper()) || x.HallName.ToUpper().Contains(searchTerm.ToUpper()) || x.Img3.ToUpper().Contains(searchTerm.ToUpper()));
                    return View(await modelContext.ToListAsync());
                }
            }
            catch (Exception)
            {
                return RedirectToAction("UserIndex", "Hall", Id);
            }


            /*Include(h => h.Img3).Include(h => h.HallName).*/


            //if (searchBy == "HallName")
            //{

            //    //ViewBag.hallSearch = _context.Hall.Where(x => x.).
            //    //var result = await halls.Where(x => x.HallName == search || search == null).ToList();
            //    return View(/*result*/);
            //}
            //else
            //{
            //    //return View(db.Employees.Where(x => x.Name.StartsWith(search) || search == null).ToList());jkjk
            //}


            //----------
            //ViewBag.hallId =_context.Halls.Select(x=>x.IdHall);
            //------------

            //return View(halls);
        }



    }
}
