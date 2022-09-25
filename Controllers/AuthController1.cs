using HallBookingProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HallBookingProject.Controllers
{
    public class AuthController1 : Controller

    {
        private ModelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;



        public AuthController1(ModelContext context)
        {
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register([Bind("IdUser,Fname,Lname,Email,Phonenum,ProfilePic,Roleid")] User0 user0 ,string password, string email)
        {
            //if (email == _context.Logins.Where(x => x.Email == email).SingleOrDefault().Email)
            //{
            //    ModelState.AddModelError(email, "The Email already used ! ");
            //    return View();
            //}
            if (ModelState.IsValid)
            {
                
                user0.Roleid = 2;
                _context.Add(user0);

                _context.SaveChangesAsync();
                

            
                Login login = new Login();
                login.Email = email;
                login.Password = password;
                login.Roleeid = 2;
                login.Userid = user0.IdUser;
                _context.Add(login);
                _context.SaveChanges();

                return RedirectToAction(nameof(Register));

            }
            //ViewData["Roleid"] = new SelectList(_context.Userroles, "Roleid", "RoleName", user0.Roleid);




            return View(user0);

          
        }

        // GET: User0/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user0 = _context.User0s.Find(id);
            if (user0 == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.Userroles, "Roleid", "RoleName", user0.Roleid);
            return View(user0);
        }


        [HttpPost]
        public IActionResult Login ([Bind("Email", "Password")] Login login)
        {
            var user = _context.Logins.Where(x => x.Email == login.Email && x.Password == login.Password).SingleOrDefault();
            if(user != null)
            {
                switch(user.Roleeid)
                {
                    case 1:
                        HttpContext.Session.SetInt32("AdminId", (int)user.Userid);
                        return RedirectToAction("Index", "AdminController1");

                        //-------------------------------------------

                    case 2:
                        HttpContext.Session.SetInt32("CustmerId", (int)user.Userid);
                     
                        return RedirectToAction("Index", "Home");
                }
            }

          
                ModelState.AddModelError(login.Email, "Incorrect Emailor Passwoed ");

          
          
            return View();
        }


        public IActionResult Login()
        {
             return View();
        }
    }
}

