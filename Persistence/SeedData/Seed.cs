using Bogus; // تأكد من تحميل الباكدج دي
using Core.DomainLayer.Entities;
using DomainLayer.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.SeedData
{
    public class Seed(UserManager<AppUser> _userManager,
                      RoleManager<IdentityRole> _roleManager,
                      ApplicationDbContext _context) : ISeed
    {
        public async Task SeedAsync()
        {
            if ((await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
            }

            var roles = new List<string> { "Admin", "Doctor", "Patient" };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }

            if (!await _context.Clinics.AnyAsync())
            {
                var clinics = new List<Clinic>
                {
                    new Clinic { Name = "Heart Center", Address = "Building A" , Phone = "12345678912"},
                    new Clinic { Name = "Dental Care", Address = "Building B" ,Phone = "12345678912"},
                    new Clinic { Name = "Skin Clinic", Address = "Building C" , Phone = "12345678912"}
                };
                await _context.Clinics.AddRangeAsync(clinics);
                await _context.SaveChangesAsync();
            }

        
            var existingPatients = await _userManager.GetUsersInRoleAsync("Patient");
            if (!existingPatients.Any())
            {
                var patientFaker = new Faker<AppUser>()
                    .RuleFor(u => u.FullName, f => f.Name.FullName())
                    .RuleFor(u => u.Email, f => f.Internet.Email())
                    .RuleFor(u => u.UserName, (f, u) => u.Email)
                    .RuleFor(u => u.EmailConfirmed, _ => true);

                var fakePatients = patientFaker.Generate(50); 

                foreach (var user in fakePatients)
                {
                    var result = await _userManager.CreateAsync(user, "P@ssword123");
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Patient");
                    }
                }
            }

            if (!await _context.Doctors.AnyAsync())
            {
                var clinics = await _context.Clinics.ToListAsync();
                var doctorFaker = new Faker();

                for (int i = 0; i < 10; i++) 
                {
                    var docUser = new AppUser
                    {
                        FullName = "Dr. " + doctorFaker.Name.LastName(),
                        Email = doctorFaker.Internet.Email(),
                        UserName = doctorFaker.Internet.Email(),
                        EmailConfirmed = true
                    };

                    var result = await _userManager.CreateAsync(docUser, "Doctor@123");
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(docUser, "Doctor");

                        var doctorProfile = new Doctor
                        {
                            AppUserId = docUser.Id,
                            Specialty = doctorFaker.Name.JobTitle(),
                            ConsultationFee = doctorFaker.Random.Decimal(100, 1000),
                            ClinicId = doctorFaker.PickRandom(clinics).Id
                        };
                        await _context.Doctors.AddAsync(doctorProfile);
                    }
                }
                await _context.SaveChangesAsync();
            }

            if (!await _context.Appointments.AnyAsync())
            {
                var patientUsers = await _userManager.GetUsersInRoleAsync("Patient");
                var doctors = await _context.Doctors.ToListAsync();

                if (patientUsers.Any() && doctors.Any())
                {
                    // Faker configuration for Appointments
                    var appointmentFaker = new Faker<Appointment>()
                        .RuleFor(a => a.PatientId, f => f.PickRandom(patientUsers).Id) // ربط بـ AppUser Id
                        .RuleFor(a => a.DoctorId, f => f.PickRandom(doctors).Id)     // ربط بـ Doctor Id (int)
                        .RuleFor(a => a.StartTime, f => f.Date.Soon(30))             // مواعيد خلال الـ 30 يوم الجايين
                        .RuleFor(a => a.EndTime, (f, a) => a.StartTime.AddMinutes(30)) // مدة الكشف 30 دقيقة
                        .RuleFor(a => a.Status, f => f.PickRandom<AppointmentStatus>())
                        .RuleFor(a => a.Notes, f => f.Lorem.Sentence());

                    var appointments = appointmentFaker.Generate(100); // توليد 100 موعد

                    await _context.Appointments.AddRangeAsync(appointments);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}