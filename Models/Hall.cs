using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HallBookingProject.Models
{
    public partial class Hall
    {
        public Hall()
        {
            Reservations = new HashSet<Reservation>();
        }

        public decimal IdHall { get; set; }
        [Required]
        [Display(Name = "Hall Name")]
        public string HallName { get; set; }

        [Display(Name = "Description")]
        public string HallDesc { get; set; }

        [Display(Name = "Imge 1")]
        public string Img1 { get; set; }

        [Display(Name = "Rrice")]
        public string Img2 { get; set; }

        [Display(Name = "Location")]
        public string Img3 { get; set; }
        public decimal Catid { get; set; }
       // public string Price { get; set; }


        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public virtual Categoryy Cat { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
