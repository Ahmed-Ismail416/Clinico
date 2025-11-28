using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Core.DomainLayer.Entities;

public class AppUser : IdentityUser
{
    [Required, MaxLength(100)]
    public string FullName { get; set; } = null!;

    public string? ProfilePictureUrl { get; set; }

    // Navigation
    public ICollection<Appointment> AppointmentsAsPatient { get; set; } = new List<Appointment>();
    public Doctor? DoctorProfile { get; set; }
}