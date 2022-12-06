using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace HallBookingProject.Models
{
    public partial class Feedback
    {
        public decimal IdFeed { get; set; }

        [Display(Name = "User Name")]
        public string FullName { get; set; }

        [Display(Name = "Statuse")]
        public string Email { get; set; }

        [Display(Name = "User Feedback")]
        public string Feedback1 { get; set; }

        [Display(Name = "User Email")]
        public decimal? Feedid { get; set; }

        [Display(Name = "User Email")]
        public virtual User0 Feed { get; set; }
    }
}
