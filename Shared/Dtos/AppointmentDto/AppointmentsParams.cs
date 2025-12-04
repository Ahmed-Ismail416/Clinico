using Core.DomainLayer.Entities;

public class AppointmentParams
{
  

    // Filtering
    public string? PatientName { get; set; }
    public string? DoctorName { get; set; }
    public DateTime? StartTime { get; set; }
    public AppointmentStatus? Status { get; set; }

    // Sorting
    public string? Sort { get; set; }

    // Pagination
    public int PageIndex { get; set; } = 1;

    private const int MaxPageSize = 20;
    int pagesize = 5;
    public int PageSize 
    {
        set => pagesize = value > MaxPageSize ? MaxPageSize : value;
        get => pagesize;
    }

}
