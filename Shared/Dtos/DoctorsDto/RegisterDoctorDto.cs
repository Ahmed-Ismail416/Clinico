using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.DoctorDto
{
    public class RegisterDoctorDto
    {
        [Required] public string FullName { get; set; } = default!;
        [Required, EmailAddress] public string Email { get; set; } = default!;
        [Required] public string Password { get; set; } = default!;
        [Phone] public string PhoneNumber { get; set; } = default!;
        [Required] public string Specialty { get; set; } = default!;
        [Range(0, double.MaxValue)] public decimal ConsultationFee { get; set; }
        [Required] public int ClinicId { get; set; }
        public IFormFile? Image { get; set; }
    }
}
