using Core.DomainLayer.Entities;
using DomainLayer.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Persistence.SeedData
{
    public  class Seed(UserManager<AppUser> _userManager,
                    RoleManager<IdentityRole> _roleManager,
                    ApplicationDbContext _context):ISeed
    {
        public async Task SeedAsync()
        {
            //Migration
            var pendingMigration = await _context.Database.GetPendingMigrationsAsync();
            if (pendingMigration.Any())
            {
                await _context.Database.MigrateAsync();
            }



            var roles = new List<string> { "Admin", "Doctor", "Patient" };
            foreach(var role in roles)
            {
                if(!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }

            // Seed Admin User
            var adminEmail = "admin@clinico.com";
            if (await _userManager.FindByEmailAsync(adminEmail) == null)
            {
                var Admin = new AppUser()
                {
                    UserName = "Admin",
                    Email = adminEmail,
                    FullName = "Clinico Admin"
                };
                await _userManager.CreateAsync(Admin, "Admin@123");
                await _userManager.AddToRoleAsync(Admin, "Admin");
            }
            // Seed Doctor User
            var doctorEmail = "doctor@clinico.com";
            if (await _userManager.FindByEmailAsync(doctorEmail) == null)
            {
                var Doctor = new AppUser()
                {

                    UserName = doctorEmail,
                    Email = doctorEmail,
                    FullName = "Dr. Mohamed Ali"
                };
                await _userManager.CreateAsync(Doctor, "Doctor@123");
                await _userManager.AddToRoleAsync(Doctor, "Doctor");
                // Profil Doctor
                // إنشاء Doctor Profile
                var doctor = new Doctor
                {
                    AppUserId = Doctor.Id,
                    Specialty = "Cardiology",
                    ConsultationFee = 500,
                    ClinicId = 1 // هيتم إنشاؤه لاحقًا
                };
            }
          
        }
    }
}
