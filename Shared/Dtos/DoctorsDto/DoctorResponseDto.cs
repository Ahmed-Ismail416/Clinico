using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.DoctorsDto
{
    public class DoctorResponseDto
    {
        public int Id { get; set; }
        public string AppUserId { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string? ProfilePictureUrl { get; set; } // ضفنا الصورة
        public string Specialty { get; set; } = default!;
        public decimal ConsultationFee { get; set; }
        public string ClinicName { get; set; } = default!;
    }
}
