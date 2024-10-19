namespace PerfumeStore.API.RequestModel
{
    public class CartRequestModel
    {
        public Guid UserId { get; set; }

        public Guid PerfumeId { get; set; }

        public int Quantity { get; set; }
    }
}
