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
        public AppointmentWithDetailsSpecification(AppointmentParams p) : base(a =>

           (
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
            AddIncludes();
            // ---------------- Sorting ----------------
            ApplySorting(p);

            // ---------------- Pagination ----------------
            ApplyPaging(p.PageSize, p.PageIndex);

        }

        private void ApplySorting(AppointmentParams p)
        {
            if (string.IsNullOrEmpty(p.Sort))
            {
                AddOrderBy(a => a.StartTime);
            }
            else
            {
                switch (p.Sort.ToLower())
                {
                    case "doctor":
                        AddOrderBy(a => a.Doctor.AppUser.FullName);
                        break;

                    case "-doctor":
                        AddOrderByDescending(a => a.Doctor.AppUser.FullName);
                        break;

                    case "patient":
                        AddOrderBy(a => a.Patient.FullName);
                        break;

                    case "-patient":
                        AddOrderByDescending(a => a.Patient.FullName);
                        break;

                    case "start":
                        AddOrderBy(a => a.StartTime);
                        break;

                    case "-start":
                        AddOrderByDescending(a => a.StartTime);
                        break;

                    case "status":
                        AddOrderBy(a => a.Status);
                        break;

                    case "-status":
                        AddOrderByDescending(a => a.Status);
                        break;

                    default:
                        AddOrderBy(a => a.StartTime);
                        break;
                }
            }
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
