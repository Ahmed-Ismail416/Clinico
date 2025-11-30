using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Core.DomainLayer.Entities;



namespace Services
{
    public class AuthSerivce(UserManager<AppUser> _userManager,
                             SignInManager<AppUser> _signInManager,
                             IConfiguration _configuration) : IAuthService
    {
        public async Task<AuthResponseDto> LoginAsync(LoginDto login)
        {
            //check email
            var user = await _userManager.FindByEmailAsync(login.Email) ?? throw new Exception("not valid");
            if(user == null)
                throw new UnauthorizedAccessException("invalid Credentials");
            //vertification the password
            var result = await _userManager.CheckPasswordAsync(user, login.Password);
            if (!result)
                throw new UnauthorizedAccessException("invalid Credentials");
            return await GenerateTokenAsync(user);
           
            
           

        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto register)
        {
            var user = new AppUser()
            {
                Email = register.Email,
                FullName = register.FullName,
                UserName = register.Email

            };
            var result = await _userManager.CreateAsync(user, register.Password);
            if(!result.Succeeded)
            {
                var errors = String.Join(",", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Registration failed: {errors}"); //throw new Exception(errors);
            }
            return await GenerateTokenAsync(user);

        }

        private async Task<AuthResponseDto> GenerateTokenAsync(AppUser user)
        {
            var claims = new List<Claim>{
                new Claim (ClaimTypes.Email, user.Email!),
                new Claim (ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var roles = await _userManager.GetRolesAsync(user);

            var key = _configuration.GetSection("Jwt")["Key"];
            var KeyBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
            var credentials = new SigningCredentials(KeyBytes, SecurityAlgorithms.HmacSha512Signature);

            var Token = new JwtSecurityToken(
                issuer: _configuration.GetSection("Jwt")["Issuer"],
                audience: _configuration.GetSection("Jwt")["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(_configuration.GetValue<int>("Jwt:ExpirationMinutes")),
                signingCredentials: credentials
                );
            var TokeString = new JwtSecurityTokenHandler().WriteToken(Token);
            return new AuthResponseDto()
            {
                Token = TokeString,
                ExpiresAt = Token.ValidTo,
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email!
            };
            
        }
    }
}
