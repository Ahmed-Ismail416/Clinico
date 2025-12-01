using DomainLayer.Contracts;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ServiceApplicationRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IClinicService, ClinicService>();

            // Apply Mapster Configurations
            MappingConfig.BaseUrl = configuration["BaseUrl"];

            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(MappingConfig).Assembly);
            // Add Mapster as a service
            services.AddSingleton(config);
            services.AddScoped<MapsterMapper.IMapper, MapsterMapper.Mapper>();


            return services;
        }
    }
}