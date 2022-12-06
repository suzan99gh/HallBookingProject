using HallBookingProject.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HallBookingProject.Controllers
{
    public class AdminController1 : Controller
    {
        private ModelContext _context;

        public AdminController1(ModelContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            /*int?*/
           int? id = HttpContext.Session.GetInt32("AdminId");
            var user = _context.User0s.Where(x => x.IdUser == id).FirstOrDefault(); 

            //ViewBag.Users = _context.User0s.ToList().Count();
            //-------------------------
            //         var user = _context.User0s.Include(x => x.Role);
            //----------------------


            ViewBag.user = _context.User0s.ToList();
            //var categories = _context.Categoryys.ToList();

            var hall = _context.Halls.ToList();
            var reservition = _context.Reservations.ToList();
            var hallName = _context.Halls.Select(x => x.HallName).ToList();
            List<int> Count = new List<int>();
            foreach (var item in hallName)
            {
                Count.Add(reservition.Count(x => x.Hall.HallName == item));
            }

            ViewBag.hallName = hallName;
            ViewBag.Count = Count;


            //ViewData["Roleid"] = new SelectList(_context.Userroles, "Roleid", "RoleName");
            return View(user);
        }

        public IActionResult UserProfile()
        {

            int? id = HttpContext.Session.GetInt32("AdminId");
            var user = _context.User0s.Where(x => x.IdUser == id).FirstOrDefault();
            ViewBag.CustmerPic = HttpContext.Session.GetString("AdminPic");

            ViewBag.UserId = id;
            return View(user);
        }

        public IActionResult JoinTables()
        {
            var user0 = _context.User0s.ToList();
            var reservation = _context.Reservations.ToList();
            var hall = _context.Halls.ToList();
            var categoryy = _context.Categoryys.ToList();

            var someTabels = from cat in categoryy
                             join h in hall on cat.IdCat equals h.Catid
                             join r in reservation on h.IdHall equals r.Hallid
                             join u in user0 on r.Userid equals u.IdUser
                             select new JoinTables { categoryy = cat, hall = h, reservation = r, user0 = u };
            ViewBag.tabel = someTabels;

            return View();
        }


        [HttpGet]
        public IActionResult Report()
        {
            var user0 = _context.User0s.ToList();
            var reservation = _context.Reservations.ToList();
            var hall = _context.Halls.ToList();
            var categoryy = _context.Categoryys.ToList();

            var someTabels = from cat in categoryy
                             join h in hall on cat.IdCat equals h.Catid
                             join r in reservation on h.IdHall equals r.Hallid
                             join u in user0 on r.Userid equals u.IdUser
                             select new { Categoryy = cat, Hall = h, Reservation = r, User0 = u };

            var modelContext = _context.Reservations.Include(x => x.Hall).Include(p => p.User).ToList();
            //ViewBag.Totalhalls = modelContext.Sum(Int64.Parse(x =>x.Hall.Img2));
            //ViewBag.Totalhalls = modelContext.Sum(x => x.Hallid); و عمل المهندس توتال لل برايس السعر 

            //var model2 = Tuple.Create < IEnumerable < someTabels >, IEnumerable < hall >> (someTabels, modelContext);

            return View(someTabels);
        }


        [HttpPost]

        public async Task<IActionResult> Report(DateTime? StartEvent, DateTime? EndEvent)
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

        }


        public IActionResult SendEmail()
        {



            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress("Hall Finder", "s.moe12@yahoo.com");
            message.From.Add(from);
            MailboxAddress to = new MailboxAddress("User", "suzan.gh99@gmail.com");
            message.To.Add(to);
            message.Subject = "Status of your reservation";
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody =
            "<h1 style=\"color:#7fb685\"> Thank you for trusting us </h1>";
            message.Body = bodyBuilder.ToMessageBody();
            using (var clinte = new SmtpClient())
            {
                clinte.Connect("smtp.mail.yahoo.com", 465, true);
                clinte.Authenticate("s.moe12@yahoo.com", "rxlhovtglvjibneg");

                clinte.Send(message);
                clinte.Disconnect(true);

            }

            return View();
        }
    }
}






























































//هون  الزيادة عن الريبورت اذا بدي احط التوتال ل اشي معين 
//    مثلا توتال لل سعر

//var user0 = _context.User0s.ToList();
//var reservation = _context.Reservations.ToList();
//var hall = _context.Halls.ToList();
//var categoryy = _context.Categoryys.ToList();

//var someTabels = from cat in categoryy
//                 join h in hall on cat.IdCat equals h.Catid
//                 join r in reservation on h.IdHall equals r.Hallid
//                 join u in user0 on r.Userid equals u.IdUser
//                 select new { Categoryy = cat, Hall = h, Reservation = r, User0 = u };



//var modelContext = _context.Reservations.Include(x => x.Hall).Include(x => x.User);
//if (StartEvent == null && EndEvent == null)
//{
//    //هذول الي فوق نفسهم
//    //ViewBag.Totalhalls = modelContext.Sum(Int64.Parse(x =>x.Hall.Img2));
//    //ViewBag.Totalhalls = modelContext.Sum(x => x.Hallid); و عمل المهندس توتال لل برايس السعر 
//    //var model2 = Tuple.Create < IEnumerable < someTabels >, IEnumerable < hall >> (someTabels, await modelContext.ToListAsync());
//    return View(modelContext);
//}



//else if (StartEvent == null && EndEvent != null)
//{

//    //هذول الي فوق نفسهم
//    //ViewBag.Totalhalls = modelContext.Sum(Int64.Parse(x =>x.Hall.Img2));
//    //ViewBag.Totalhalls = modelContext.Sum(x => x.Hallid); و عمل المهندس توتال لل برايس السعر 

//    //var model2 = Tuple.Create<IEnumerable<someTabels>, IEnumerable<hall>>(someTabels, await modelContext.Where(x => x.EndEvent.Value.Date == EndEvent). ToListAsync());

//    return View(/*model2*/);
//}

//else if (StartEvent != null && EndEvent == null)
//{

//    //هذول الي فوق نفسهم
//    //ViewBag.Totalhalls = modelContext.Sum(Int64.Parse(x =>x.Hall.Img2));
//    //ViewBag.Totalhalls = modelContext.Sum(x => x.Hallid); و عمل المهندس توتال لل برايس السعر 

//    //var model2 = Tuple.Create<IEnumerable<someTabels>, IEnumerable<hall>>(someTabels, await modelContext.Where(x => x.StartEvent.Value.Date == StartEvent). ToListAsync());

//    return View(/*model2*/);



//}

//else
//{

//    //هذول الي فوق نفسهم
//    //ViewBag.Totalhalls = modelContext.Sum(Int64.Parse(x =>x.Hall.Img2));
//    //ViewBag.Totalhalls = modelContext.Sum(x => x.Hallid); و عمل المهندس توتال لل برايس السعر 

//    //var model2 = Tuple.Create<IEnumerable<someTabels>, IEnumerable<hall>>(someTabels, await modelContext.Where(x => x.StartEvent >= StartEvent && x.EndEvent <= EndEvent). ToListAsync());

//    return View(/*model2*/);

//}

