using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class ClinicNotFoundException(int id) : NotFoundException($"Clinic with ID '{id}' was not found.")
    {
    }
}
