using System;
using System.Collections.Generic;

#nullable disable

namespace HallBookingProject.Models
{
    public partial class Contact
    {
        public Contact()
        {
            Homes = new HashSet<Home>();
        }

        public decimal IdContact { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public virtual ICollection<Home> Homes { get; set; }
    }
}
