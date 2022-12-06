using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HallBookingProject.Models
{
    public partial class Home
    {
        public decimal IdHome { get; set; }
        [Display(Name = "Cover Image")]
        public string Img1 { get; set; }
       
        [Display(Name = "Home Description")]
        public string Img2 { get; set; }
        public string Img3 { get; set; }

        public decimal? Aboutid { get; set; }
        public decimal? Contactid { get; set; }


        [NotMapped]
        public IFormFile Homeimg { get; set; }

        public virtual AboutU About { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
