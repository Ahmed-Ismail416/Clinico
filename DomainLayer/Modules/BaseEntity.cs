namespace Core.DomainLayer.Entities;

public class BaseEntity<Tkey>
{
    public Tkey Id { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}