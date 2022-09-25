using System;
using System.Collections.Generic;

#nullable disable

namespace HallBookingProject.Models
{
    public partial class Userrole
    {
        public Userrole()
        {
            Logins = new HashSet<Login>();
            User0s = new HashSet<User0>();
        }

        public decimal Roleid { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<User0> User0s { get; set; }
    }
}
