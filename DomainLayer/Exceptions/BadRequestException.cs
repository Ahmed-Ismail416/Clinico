using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class BadRequestException(IEnumerable<string> errors, string message = "Validation Failed") : Exception(message)
    {
        public IEnumerable<string> Errors { get; set; } = errors;
    }
}
