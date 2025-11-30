using Clinico.Extensions;
using Clinico.MiddleWares;
using DomainLayer.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using System.Threading.Tasks;

namespace Clinico
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services

            builder.Services.AddControllers();
            builder.Services.AddInfrastructerServices(builder.Configuration);
            builder.Services.AddJWTService(builder.Configuration);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddWebApiServices();

            #endregion



            var app = builder.Build();

            // Seed Data
            using var scope = app.Services.CreateScope();
            var objOfDataSeeding = scope.ServiceProvider.GetRequiredService<ISeed>();
            await objOfDataSeeding.SeedAsync();

            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clinico API v1");
                    // اختياري: c.RoutePrefix = "swagger"; // ده الديفولت
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}




