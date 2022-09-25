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
    public class ReservationsController : Controller
    {
        private readonly ModelContext _context;

        public ReservationsController(ModelContext context)
        {
            _context = context;
        }


        [HttpGet]
        // GET: Reservations
        public async Task<IActionResult> Index()

        {
            ViewBag.Halls = _context.Halls.ToList().Count();

            //ViewBag.booked = _context.Reservations.Where(x => x.Hallid).FirstOrDefault(); 

            //var hallsCount = HttpContext.Session.GetString("Hall_Count");

            //ViewBag.unbooked = ViewBag.booked - hallsCount;



            var modelContext = _context.Reservations.Include(r => r.Hall).Include(r => r.Pay).Include(r => r.User);
            return View(await modelContext.ToListAsync());
        }


        [HttpPost]

        public async Task<IActionResult> Index(DateTime? StartEvent, DateTime? EndEvent , string status)
        {

            var modelContext = _context.Reservations.Include(x => x.Hall).Include(x => x.User);
            if (StartEvent == null && EndEvent == null)
            {
                Reservation book = new Reservation();
                book.Status = status;
              
                _context.Add(book);
                _context.SaveChanges();



                return View(modelContext);
            }



            else if (StartEvent == null && EndEvent != null)
            {
                Reservation book = new Reservation();
                book.Status = status;

                _context.Add(book);
                _context.SaveChanges();

                var result = await modelContext.Where(x => x.EndEvent.Value.Date == EndEvent).ToListAsync();
                return View(result);
            }

            else if (StartEvent != null && EndEvent == null)
            {
                Reservation book = new Reservation();
                book.Status = status;

                _context.Add(book);
                _context.SaveChanges();
                var result = await modelContext.Where(x => x.StartEvent.Value.Date == StartEvent).ToListAsync();
                return View(result);

            }

            else
            {
                var result = await modelContext.Where(x => x.StartEvent >= StartEvent && x.EndEvent <= EndEvent).ToListAsync();
                Reservation book = new Reservation();
                book.Status = status;

                _context.Add(book);
                _context.SaveChanges();
                return View(result);
            }

        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Hall)
                .Include(r => r.Pay)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.IdBook == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["Hallid"] = new SelectList(_context.Halls, "IdHall", "HallName");
            ViewData["Payid"] = new SelectList(_context.Visas, "IdPayment", "CardName");
            ViewData["Userid"] = new SelectList(_context.User0s, "IdUser", "Email");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(decimal id ,[Bind("IdBook,Status,StartEvent,EndEvent,Hallid,Userid,Payid")] Reservation reservation  )
        {
            if (ModelState.IsValid)
            {

                //Hall hall = new Hall();
                // hall.IdHall = idhaall;

                //ViewBag.hallId = _context.Halls.Select(x => x.IdHall);
                reservation.Status = "Requested";
                reservation.Hallid = id;
                reservation.Userid = HttpContext.Session.GetInt32("CustmerId");
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Hallid"] = new SelectList(_context.Halls, "IdHall", "HallName", reservation.Hallid);
            ViewData["Payid"] = new SelectList(_context.Visas, "IdPayment", "CardName", reservation.Payid);
            ViewData["Userid"] = new SelectList(_context.User0s, "IdUser", "Email", reservation.Userid);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["Hallid"] = new SelectList(_context.Halls, "IdHall", "HallName", reservation.Hallid);
            ViewData["Payid"] = new SelectList(_context.Visas, "IdPayment", "CardName", reservation.Payid);
            ViewData["Userid"] = new SelectList(_context.User0s, "IdUser", "Email", reservation.Userid);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdBook,Status,StartEvent,EndEvent,Hallid,Userid,Payid")] Reservation reservation)
        {
            if (id != reservation.IdBook)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.IdBook))
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
            ViewData["Hallid"] = new SelectList(_context.Halls, "IdHall", "HallName", reservation.Hallid);
            ViewData["Payid"] = new SelectList(_context.Visas, "IdPayment", "CardName", reservation.Payid);
            ViewData["Userid"] = new SelectList(_context.User0s, "IdUser", "Email", reservation.Userid);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Hall)
                .Include(r => r.Pay)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.IdBook == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(decimal id)
        {
            return _context.Reservations.Any(e => e.IdBook == id);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Book([Bind("IdBook,Status,StartEvent,EndEvent,Hallid,Userid,Payid")] Reservation reservation)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        reservation.Status = "R";
        //        _context.Add(reservation);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["Hallid"] = new SelectList(_context.Halls, "IdHall", "HallName", reservation.Hallid);
        //    ViewData["Payid"] = new SelectList(_context.Visas, "IdPayment", "CardName", reservation.Payid);
        //    ViewData["Userid"] = new SelectList(_context.User0s, "IdUser", "Email", reservation.Userid);
        //    return View(reservation);
        //}



        public async Task<IActionResult> Booking([Bind("IdBook,Status,StartEvent,EndEvent,Hallid,Userid,Payid")] Reservation reservation)
        {
            //if (ModelState.IsValid)
            //{


            //    reservation.Status = "Requested";
            //    _context.Add(reservation);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            ViewData["Hallid"] = new SelectList(_context.Halls, "IdHall", "HallName", reservation.Hallid);
            ViewData["Payid"] = new SelectList(_context.Visas, "IdPayment", "CardName", reservation.Payid);
            ViewData["Userid"] = new SelectList(_context.User0s, "IdUser", "Email", reservation.Userid);
            return View();
        }

        public async Task<IActionResult> Request ([Bind("IdBook,Status,StartEvent,EndEvent,Hallid,Userid,Payid")] Reservation reservation)
        {

            reservation.Status = "Requested";
            _context.Add(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            //return View ();
        }


    }
}
