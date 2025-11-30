using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModels
{
    public class ValidationErrorToReturn
    {
        //status code, message, ValidationErrors
        public int StatusCode { get; set; } = 400;
        public string Message { get; set; } = default!;
        public IEnumerable<ValidationError> ValidationErrors { get; set; } = [];
    }
}
