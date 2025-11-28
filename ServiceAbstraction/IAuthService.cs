using Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAuthService
    {
        public Task<AuthResponseDto> RegisterAsync(RegisterDto register);
        public Task<AuthResponseDto> LoginAsync(LoginDto login);
    }
}
