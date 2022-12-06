using System;
using System.Collections.Generic;

#nullable disable

namespace HallBookingProject.Models
{
    public partial class Reservation
    {
        public decimal IdBook { get; set; }
        public string Status { get; set; }
        public DateTime? StartEvent { get; set; }
        public DateTime? EndEvent { get; set; }
        public decimal? Hallid { get; set; }
        public decimal? Userid { get; set; }
        public decimal? Payid { get; set; }

        public virtual Hall Hall { get; set; }
        public virtual Visa Pay { get; set; }
        public virtual User0 User { get; set; }
    }
}
