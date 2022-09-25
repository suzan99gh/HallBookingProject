using System;
using System.Collections.Generic;

#nullable disable

namespace HallBookingProject.Models
{
    public partial class Payment
    {
        public decimal IdPay { get; set; }
        public string Pay { get; set; }
        public DateTime? PayDate { get; set; }
        public decimal? StdPay { get; set; }
        public decimal? CorCost { get; set; }
    }
}
