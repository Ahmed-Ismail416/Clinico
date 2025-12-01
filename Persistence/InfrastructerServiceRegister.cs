using Core.DomainLayer.Entities;
using DomainLayer.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.SeedData;
using Persistence.UnitOfWorkPatter;
using ServiceAbstraction;
using Services;


namespace Persistence
{
    public static class InfrastructerServiceRegister 
    {
        public static IServiceCollection AddInfrastructerServices(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));


            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<ISeed, Seed>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

    }
}
