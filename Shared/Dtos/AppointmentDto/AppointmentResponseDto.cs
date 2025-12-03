using Core.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.AppointmentDto
{
    public class AppointmentResponseDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public AppointmentStatus Status { get; set; }
        public string Notes { get; set; } = default!;

        // بيانات الدكتور
        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = default!;
        public string ClinicName { get; set; } = default!;

        // بيانات المريض (عشان الدكتور لما يشوف مواعيده يعرف مين المريض)
        public string PatientName { get; set; } = default!;
    }

    
}
