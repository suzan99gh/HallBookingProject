using System;
using System.Collections.Generic;

#nullable disable

namespace HallBookingProject.Models
{
    public partial class Categoryy
    {
        public Categoryy()
        {
            Halls = new HashSet<Hall>();
        }

        public decimal IdCat { get; set; }
        public string CatName { get; set; }
        //public string CatImg { get; set; }

        public virtual ICollection<Hall> Halls { get; set; }
    }
}
