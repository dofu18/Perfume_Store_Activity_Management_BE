namespace Perfume_Store_Activity_Management_Backend.src.Domain.Order;

using Perfume_Store_Activity_Management_Backend.src.Domain.User;

public class Order
{
    public Guid OrderId { get; set; }
    public Guid CartId { get; set; }
    public string Status { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }

    public Cart Cart { get; set; }
    public User CreatedByUser { get; set; }
}
