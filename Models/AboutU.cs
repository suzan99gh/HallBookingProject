using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace HallBookingProject.Models
{
    public partial class AboutU
    {
        public AboutU()
        {
            Homes = new HashSet<Home>();
        }

        public decimal IdAbout { get; set; }
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public long? Phonenum { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Home> Homes { get; set; }
    }
}
