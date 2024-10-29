using AutoMapper;
using PerfumeStore.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Service.BusinessModel
{
    public class OrderModel
    {
        //public Guid OrderId { get; set; }

        //public Guid UserId { get; set; }

        //public DateTime? OrderDate { get; set; }

        //public decimal TotalAmount { get; set; }

        public string? Status { get; set; }

        public string? PaymentMethod { get; set; }

        public List<OrderItems> OrderItems { get; set; }

    }

    public class OrderItems
    {
        public Guid PerfumeId { get; set; }
        public int Quantity { get; set; }
        //public decimal Price { get; set; }
    }

    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderModel, Order>()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ReverseMap();

            // Map between OrderItemModel and OrderItem
            CreateMap<OrderItems, OrderItem>()
                .ReverseMap();
        }
    }
}
