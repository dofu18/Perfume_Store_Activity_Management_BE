namespace Perfume_Store_Activity_Management_Backend.src.Domain.Payment;

using Perfume_Store_Activity_Management_Backend.src.Domain.User;
using Perfume_Store_Activity_Management_Backend.src.Domain.Order;

public class Transaction
{
    public Guid TransactionId { get; set; }
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Message { get; set; }
    public string Resource { get; set; }
    public string Meta { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }

    public Order Order { get; set; }
    public User CreatedByUser { get; set; }
}
