using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace HallBookingProject.Models
{
    public partial class Locationn
    {

        [Key]
        public decimal Locationid { get; set; }
        public string LocationName { get; set; }
        public string Pic { get; set; }
    }
}
