using System;
using System.Collections.Generic;

#nullable disable

namespace HallBookingProject.Models
{
    public partial class Visa
    {
        public Visa()
        {
            Reservations = new HashSet<Reservation>();
        }

        public decimal IdPayment { get; set; }
        public string CardName { get; set; }
        public long CardNumber { get; set; }
        public byte Cvc { get; set; }
        public byte ExprDate { get; set; }
        public decimal? Balance { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
