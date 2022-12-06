using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HallBookingProject.Models
{
    public partial class User0
    {
        public User0()
        {
            Feedbacks = new HashSet<Feedback>();
            Logins = new HashSet<Login>();
            Reservations = new HashSet<Reservation>();
        }

        public decimal IdUser { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public long? Phonenum { get; set; }
        public string ProfilePic { get; set; }
        public decimal? Roleid { get; set; }


        [NotMapped]
        public IFormFile ImageProfile { get; set; }


        public virtual Userrole Role { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
