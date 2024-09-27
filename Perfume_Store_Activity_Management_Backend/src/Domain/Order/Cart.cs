namespace Perfume_Store_Activity_Management_Backend.src.Domain.Order;

using Perfume_Store_Activity_Management_Backend.src.Domain.User;

public class Cart
{
    public Guid CartId { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }

    public User CreatedByUser { get; set; }
    public List<PerfumeCart> PerfumeCarts { get; set; }
}
