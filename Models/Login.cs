using System;
using System.Collections.Generic;

#nullable disable

namespace HallBookingProject.Models
{
    public partial class Login
    {
        public decimal IdLog { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public decimal? Userid { get; set; }
        public decimal? Roleeid { get; set; }

        public virtual Userrole Rolee { get; set; }
        public virtual User0 User { get; set; }
    }
}
