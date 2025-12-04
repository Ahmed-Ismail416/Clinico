using Core.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    public class AppointmentCountSpecification : BaseSpecification<Appointment, int>
    {
        public AppointmentCountSpecification(AppointmentParams p) : base(a => (
                string.IsNullOrEmpty(p.DoctorName)
                || a.Doctor.AppUser.FullName.ToLower().Contains(p.DoctorName.ToLower())
            )

            // ---------------- Patient Name Filter ----------------
            && (
                string.IsNullOrEmpty(p.PatientName)
                || a.Patient.FullName.ToLower().Contains(p.PatientName.ToLower())
            )

            // ---------------- Start Time Filter ----------------
            && (
                !p.StartTime.HasValue
                || a.StartTime.Date == p.StartTime.Value.Date
            )

            // ---------------- Status Filter ----------------
            && (
                !p.Status.HasValue
                || a.Status == p.Status.Value
            )
)
        {
            
        }
    }
}
