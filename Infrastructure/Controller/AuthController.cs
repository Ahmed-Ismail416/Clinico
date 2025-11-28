using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    public class AuthController(IAuthService _authService) : ApiBaseController
    {
        //login
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody]LoginDto loginDto)
       => Ok(await _authService.LoginAsync(loginDto));

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto registerDto)
        => Ok(await _authService.RegisterAsync(registerDto));
    }
}
