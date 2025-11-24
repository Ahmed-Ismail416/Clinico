using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Modules
{
    public class Patient : IdentityUser
    {
        public string  FullName { get; set; } = default!;
        public string  Address { get; set; } = default!;
        public ICollection<Appointment> Appointments { get; set; } = [];
    }
}
