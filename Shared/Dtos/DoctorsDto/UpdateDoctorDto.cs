using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.DoctorsDto
{
    public class UpdateDoctorDto
    {
        [Required] public string FullName { get; set; } = null!;
        [Required, EmailAddress] public string Email { get; set; } = null!;
        [Required] public string PhoneNumber { get; set; } = null!;

        [Required] public string Specialty { get; set; } = null!;
        [Range(0, double.MaxValue)] public decimal ConsultationFee { get; set; }
        [Required] public int ClinicId { get; set; }

        public IFormFile? Image { get; set; } // اختياري للتعديل
    }
}
