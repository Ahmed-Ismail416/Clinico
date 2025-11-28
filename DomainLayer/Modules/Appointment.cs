namespace Core.DomainLayer.Entities;

public class Appointment : BaseEntity<int>
{
    public string PatientId { get; set; } = null!;
    public AppUser Patient { get; set; } = null!;

    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    public string? Notes { get; set; }

    public int? PaymentId { get; set; }
    public Payment? Payment { get; set; }
}

public enum AppointmentStatus
{
    Pending,
    Confirmed,
    Completed,
    Cancelled,
    NoShow
}