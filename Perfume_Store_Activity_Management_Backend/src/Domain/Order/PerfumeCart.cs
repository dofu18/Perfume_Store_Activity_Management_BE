using Perfume_Store_Activity_Management_Backend.src.Domain.Perfume;

namespace Perfume_Store_Activity_Management_Backend.src.Domain.Order;

public class PerfumeCart
{
    public Guid PerfumeId { get; set; }
    public Guid CartId { get; set; }
    public decimal TotalPrice { get; set; }
    public int Quantity { get; set; }
    public int Discount { get; set; }

    public PerfumeEdition PerfumeEdition { get; set; }
    public Cart Cart { get; set; }
}
