using Core.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    public class AppointmentFilteringSpecification : BaseSpecification<Appointment, int>
    {
        public AppointmentFilteringSpecification(string userId, string role) : base(
            a =>
            (
                a.PatientId == userId && role == "Patient" ||
                a.Doctor.AppUserId == userId && role == "Doctor" ||
                role == "Admin"
            )
            )
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            AddInclude(a => a.Doctor);
            AddInclude(a => a.Patient);
            AddInclude(a => a.Doctor.AppUser);
            AddInclude(a => a.Doctor.Clinic);
        }
    }
}
