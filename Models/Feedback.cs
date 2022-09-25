using System;
using System.Collections.Generic;

#nullable disable

namespace HallBookingProject.Models
{
    public partial class Feedback
    {
        public decimal IdFeed { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Feedback1 { get; set; }
        public decimal? Feedid { get; set; }

        public virtual User0 Feed { get; set; }
    }
}
