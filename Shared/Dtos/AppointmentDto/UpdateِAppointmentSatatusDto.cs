using Core.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.AppointmentDto
{
    public class UpdateِAppointmentSatatusDto
    {
        [Required]
        public AppointmentStatus Status { get; set; }
    }
}
