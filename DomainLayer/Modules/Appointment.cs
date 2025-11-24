using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Modules
{
    public class Appointment : BaseEntity<Guid>
    {
        public DateTime AppointmentDate { get; set; } = DateTime.UtcNow;

        public AppointmentStatus Status { get; set; }

        public bool  isPaid { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = default!;
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = default!;

    }
}
