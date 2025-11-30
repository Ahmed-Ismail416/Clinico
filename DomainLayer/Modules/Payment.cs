using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DomainLayer.Entities;

public class Payment : BaseEntity<int>
{
    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public string? StripePaymentIntentId { get; set; }
}

public enum PaymentStatus
{
    Pending,
    Succeeded,
    Failed,
    Refunded
}