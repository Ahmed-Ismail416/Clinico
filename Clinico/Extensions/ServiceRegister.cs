using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared.ErrorModels;
using System.Runtime.CompilerServices;
using System.Text;

namespace Clinico.Extensions
{
    public static class ServiceRegister
    {
        public static void AddJWTService(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config["Jwt:Key"]))

                };
            });
        }

        public static IServiceCollection AddWebApiServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Any()) // ← خذ فقط الحقول اللي فيها أخطاء
                        .Select(e => new ValidationError
                        {
                            Field = e.Key,
                            Errors = e.Value.Errors.Select(er => er.ErrorMessage)
                        });
                    var errorResponce = new ValidationErrorToReturn
                    {
                        Message = "ValidationError",
                        ValidationErrors = errors
                    };
                    return new BadRequestObjectResult(errorResponce);
                };
            });

            return services;
        }   
    
    }
}
        
    

