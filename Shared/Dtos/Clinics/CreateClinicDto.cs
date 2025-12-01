using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Clinics
{
    public class CreateClinicDto
    {
        [Required(ErrorMessage = "Required!")]
        [MaxLength(150, ErrorMessage = "Name can't exceed 150 characters!")]
        public string Name { get; set; } = default!;
        [Required(ErrorMessage = "Required!")]
        public string Address { get; set; } = default!;
        [Required, Phone]
        public string Phone { get; set; } = default!;
    }
}
