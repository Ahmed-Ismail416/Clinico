using System.ComponentModel.DataAnnotations;

namespace Core.DomainLayer.Entities;

public class Clinic : BaseEntity<int>
{
    [Required, MaxLength(150)]
    public string Name { get; set; } = null!;

    [Required, MaxLength(200)]
    public string Address { get; set; } = null!;

    [Phone]
    public string Phone { get; set; } = null!;

    public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    // ملاحظة: العلاقة مع Appointment غير مباشرة عبر Doctor
}