using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [Authorize(Roles = "Doctor")]
    public class DoctorController : ApiBaseController
    {
        [HttpGet("profile")]
        public ActionResult<object> GetProfile()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(new { Message = $"Welcome Doctor {userid}" });
        }
    }
}
