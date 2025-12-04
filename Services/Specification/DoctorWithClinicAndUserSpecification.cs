using Core.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos.DoctorsDto;
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
        public DoctorWithClinicAndUserSpecification(DoctorParams _params) :  base(p =>
        // شرط البحث بالاسم
        (string.IsNullOrEmpty(_params.Search) || p.AppUser.FullName.ToLower().Contains(_params.Search.ToLower()))
        &&
        // شرط التخصص
        (string.IsNullOrEmpty(_params.Specialty) ||
         p.Specialty.ToLower().Contains(_params.Specialty.ToLower()))
        )
        
        
        {
            AddIncludes();

            switch (_params.SortBy)
            {
                case DoctorSortingOptions.NameAsc:
                    AddOrderBy(p => p.AppUser.FullName);
                    break;
                case DoctorSortingOptions.NameDesc:
                    AddOrderByDescending(o => o.AppUser.FullName);
                    break;
                case DoctorSortingOptions.SpecialtyAsc:
                    AddOrderBy(o => o.Specialty);
                    break;
                case DoctorSortingOptions.SpecialtyDesc:
                    AddOrderBy(o => o.Specialty);
                    break;
                case DoctorSortingOptions.FeeAsc:
                    AddOrderBy(o => o.ConsultationFee);
                    break;
                case DoctorSortingOptions.FeeDesc:
                    AddOrderBy(o => o.ConsultationFee);
                    break;
                default:
                    AddOrderBy(o => o.AppUserId);
                    break;
            }
            
        }
        // Include Clinic and AppUser by Id
        public DoctorWithClinicAndUserSpecification(int id) : base(d => d.Id == id )
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            AddInclude(d => d.AppUser);
            AddInclude(d => d.Clinic);
        }
    }
}
