using Core.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    internal class DoctorWithClinicAndUserSpecification : BaseSpecification<Doctor,int>
    {
        // Include Clinic and AppUser
        public DoctorWithClinicAndUserSpecification() : base(null)
        {
            AddIncludes();
        }
        // Include Clinic and AppUser by Id
        public DoctorWithClinicAndUserSpecification(int id) : base(d => d.Id == id )
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            AddInclude(d => d.Clinic);
            AddInclude(d => d.AppUser);
        }
    }
}
