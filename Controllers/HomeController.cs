using HallBookingProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HallBookingProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;



        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ILogger<HomeController> logger, ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
     

        public IActionResult Index()
        {
            //var result = _context.Homes.ToList().FirstOrDefault();

            var categories = _context.Categoryys.ToList();

            return View(categories);
        }

        public IActionResult Num()
        {
            ViewBag.result = _context.Reservations.Where(p => p.Status == "Accept").Count();

            return View();
        }




        public IActionResult CtegoryProducts(int? Id)
        {
            //----------
            //ViewBag.hallId =_context.Halls.Select(x=>x.IdHall);
            //------------
            var halls = _context.Halls.Where(x => x.Catid == Id).ToList();
            return View(halls);
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Search()
        {
            var modelContext = _context.Reservations.Include(x => x.Hall).Include(x => x.User).ToList();
            return View(modelContext);
        }



        [HttpPost]
        public async Task<IActionResult> Search(DateTime? StartEvent, DateTime? EndEvent)
        {
            var modelContext = _context.Reservations.Include(x => x.Hall).Include(x => x.User);


            if (StartEvent == null && EndEvent == null)
            {
                return View(modelContext);
            }



            else if (StartEvent == null && EndEvent != null)
            {

                var result = await modelContext.Where(x => x.EndEvent.Value.Date == EndEvent).ToListAsync();
                return View(result);
            }

            else if (StartEvent != null && EndEvent == null)
            {
                var result = await modelContext.Where(x => x.StartEvent.Value.Date == StartEvent).ToListAsync();
                return View(result);

            }

            else
            {
                var result = await modelContext.Where(x => x.StartEvent >= StartEvent && x.EndEvent <= EndEvent).ToListAsync();
                return View(result);
            }


            //return View();

        }


        public async Task<IActionResult> Create([Bind("IdHome,Img1,Img2,Img3,Aboutid,Contactid,Homeimg")] Home home)
        {
            if (ModelState.IsValid)
            {
                if (home.Homeimg != null)
                {
                    //------------------------
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + home.Homeimg.FileName;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await home.Homeimg.CopyToAsync(fileStream);
                    }

                    home.Img1 = fileName;
                }

                _context.Add(home);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        /*home)*/
        return View();
    }

}
}


