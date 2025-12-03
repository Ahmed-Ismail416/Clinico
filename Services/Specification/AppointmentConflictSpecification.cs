using Core.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    internal class AppointmentConflictSpecification : BaseSpecification<Appointment, int>
    {
        public AppointmentConflictSpecification(int doctorId, DateTime newStart, DateTime newEnd)
        :base
            (
             a => 
             a.DoctorId == doctorId && 
             a.Status != AppointmentStatus.Cancelled && 
             (newStart < a.EndTime && newEnd > a.StartTime)

             )
        {

            
        }
    }
}
