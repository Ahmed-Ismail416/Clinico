using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PictureUrlResolver (IHttpContextAccessor _accessor)
    {
        public string ResolvePictureUrl(string? pictureFileName)
        {
            if (string.IsNullOrEmpty(pictureFileName))
            {
                return string.Empty;
            }
            var request = _accessor.HttpContext?.Request;
            if (request == null)
            {
                return pictureFileName;
            }
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var fullUrl = $"{baseUrl}/{pictureFileName}";
            return fullUrl;
        }

    }
}
