using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DomainLayer.Entities;

public class Doctor : BaseEntity<int>
{
    [Required]
    public string AppUserId { get; set; } = null!;

    public AppUser AppUser { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Specialty { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal ConsultationFee { get; set; }

    public int ClinicId { get; set; }
    public Clinic Clinic { get; set; } = null!;

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<WorkingHour> WorkingHours { get; set; } = new List<WorkingHour>();
}