using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using Shared.Dtos.Auth;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Core.DomainLayer.Entities;
using DomainLayer.Exceptions;

namespace Services
{
    public class AuthService(UserManager<AppUser> _userManager,
                             IConfiguration _configuration) : IAuthService
    {
        public async Task<AuthResponseDto> LoginAsync(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
            {
                throw new UnAutherizedExcepion("Invalid email or password");
            }

            return await GenerateTokenAsync(user);
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto register)
        {
            var exists = await _userManager.FindByEmailAsync(register.Email);
            if (exists != null) throw new BadRequestException(new List<string> { "Email already exists" }, "Registration Failed");

            var user = new AppUser()
            {
                Email = register.Email,
                FullName = register.FullName,
                UserName = register.Email
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new BadRequestException(errors, "Can't Create User");
            }

            return await GenerateTokenAsync(user);
        }

        private async Task<AuthResponseDto> GenerateTokenAsync(AppUser user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = _configuration.GetSection("Jwt")["Key"];
            var keyBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
            var credentials = new SigningCredentials(keyBytes, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetSection("Jwt")["Issuer"],
                audience: _configuration.GetSection("Jwt")["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:ExpirationMinutes")),
                signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return new AuthResponseDto()
            {
                Token = tokenString,
                ExpiresAt = tokenDescriptor.ValidTo, // ValidTo is already in UTC
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email!
            };
        }
    }
}