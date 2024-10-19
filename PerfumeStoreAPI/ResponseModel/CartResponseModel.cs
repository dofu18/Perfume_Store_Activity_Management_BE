namespace PerfumeStore.API.ResponseModel
{
    public class CartResponseModel
    {
        public Guid CartId { get; set; }

        public Guid UserId { get; set; }

        public Guid PerfumeId { get; set; }

        public int Quantity { get; set; }

        public DateTime? DateAdded { get; set; }
    }
}
