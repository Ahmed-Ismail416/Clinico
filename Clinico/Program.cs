using Clinico.Extensions;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;

namespace Clinico
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services
            builder.Services.AddControllers();
            builder.Services.AddInfrastructerServices(builder.Configuration);
            builder.Services.AddJWTService(builder.Configuration);

            // Swagger / OpenAPI
            // ✅ Swagger (Swashbuckle)
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
