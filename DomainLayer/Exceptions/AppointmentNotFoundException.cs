using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public class AppointmentNotFoundException(int appointmentId)
        : NotFoundException($"Appointment with ID {appointmentId} was not found.")
    {
    }
}
