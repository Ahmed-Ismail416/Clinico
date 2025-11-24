using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Modules
{
    public class Doctor : BaseEntity<int>
    {
        public string Name { get; set; } = default!;
        public string Bio { get; set; } = default!;
        public string Specialization { get; set; } = default!;
        public decimal Price { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = [];
    }
}
