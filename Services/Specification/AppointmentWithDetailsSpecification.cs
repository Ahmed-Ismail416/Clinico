using Core.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    public class AppointmentWithDetailsSpecification : BaseSpecification<Appointment, int>
    {
        public AppointmentWithDetailsSpecification():base(null)
        {
            AddIncludes();
        }

        public AppointmentWithDetailsSpecification(int id): base(a => a.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            AddInclude(e => e.Doctor);
            AddInclude(e => e.Doctor.AppUser);
            AddInclude(e => e.Doctor.Clinic);
            AddInclude(e => e.Patient);
        }
    }
}
