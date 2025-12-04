using Core.DomainLayer.Entities;
using Shared.Dtos.DoctorsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    public class DoctorCountSpecification : BaseSpecification<Doctor, int>
    {
        public DoctorCountSpecification(DoctorParams _params) : base
            (p =>
                (String.IsNullOrEmpty(_params.Search) || p.AppUser.FullName.ToLower().Contains(_params.Search.ToLower())
                &&
                (String.IsNullOrEmpty(_params.Specialty) || p.Specialty.ToLower().Contains(_params.Specialty.ToLower())
                ))
            )
        {
            
        }
    }
}
