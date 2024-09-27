namespace Perfume_Store_Activity_Management_Backend.src.Domain.Activity;

using Perfume_Store_Activity_Management_Backend.src.Domain.Perfume;
using Perfume_Store_Activity_Management_Backend.src.Domain.User;

public class Activity
{
    public Guid ActivityId { get; set; }
    public Guid PerfumeId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime LastUsed { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }

    public User Customer { get; set; }
    public PerfumeEdition PerfumeEdition { get; set; }
}
