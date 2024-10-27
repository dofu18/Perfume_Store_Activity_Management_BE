using AutoMapper;
using PerfumeStore.Service.BusinessModel;

namespace PerfumeStore.API.RequestModel
{
    public class CreateOrderRequestModel
    {
        public decimal? Amount { get; set; } = null!;
        public required string Currency { get; set; }
        public Guid? OrderId { get; set; } = Guid.Empty;
    }

    public class CaptureOrderRequestModel
    {
        public string? OrderId { get; set; }
        public Guid UserId { get; set; }
    }

    public class OrdersRequestModelProfile : Profile
    {
        public OrdersRequestModelProfile()
        {
            CreateMap<CreateOrderRequestModel, CreateOrderRequest>()
                .ReverseMap();
        }

    }
}
