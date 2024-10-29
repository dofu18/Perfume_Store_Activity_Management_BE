using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfumeStore.Repository;
using PerfumeStore.Repository.Model;
using PerfumeStore.Service.BusinessModel;
using PerfumeStore.Service.BusinessModel.CustomResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Service.Service
{
    public class OrderService
    {
        private readonly UnitOfWork _unitOfWork;
        
        private readonly IMapper _mapper;

        private readonly PerfumeStoreContext _context;

        public OrderService(UnitOfWork unitOfWork, IMapper mapper, PerfumeStoreContext context)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
        }


        public async Task<IActionResult> HandleCreateOrder(OrderModel req, Guid userId)
        {
            try
            {
                var order = _mapper.Map<Order>(req);

                order.UserId = userId;

                decimal total = 0;

                if (req.OrderItems != null && req.OrderItems.Any())
                {
                    //order.OrderItems = (ICollection<OrderItem>)_mapper.Map<ICollection<OrderItems>>(req.OrderItems);
                    // Map the collection without explicit casting
                    order.OrderItems = _mapper.Map<ICollection<OrderItem>>(req.OrderItems);


                    foreach (var orderItem in order.OrderItems)
                    {
                        var perfume = _unitOfWork.PerfumeProducts.GetById(orderItem.PerfumeId);

                        if (perfume != null)
                        {
                            orderItem.Price = perfume.Price;

                            total = total + perfume.Price * orderItem.Quantity;
                        }
                    }
                }

                order.TotalAmount = total;

                var result = await _unitOfWork.Orders.CreateAsync(order);

                if (result != null)
                {
                    return SuccessResp.Created(order);
                }
                else
                {
                    return ErrorResp.InternalServerError("Create order failed");
                }
            }
            catch (Exception ex)
            {
                return ErrorResp.InternalServerError(ex.Message);
            }
        }

        public async Task<IActionResult> HandleCompleteOrder(Guid orderId, Guid userId)
        {
            try
            {
                var order = _unitOfWork.Orders.GetById(orderId);

                if (order == null)
                {
                    return ErrorResp.NotFound("Order not found");
                }
                else if (order.UserId != userId)
                {
                    return ErrorResp.NotFound("Invalid User");
                }
                else if (order.Status == "Completed")
                {
                    return ErrorResp.NotFound("Order was completed");
                }

                var user = order.User;

                var result = _unitOfWork.CompleteOrder(order, user);

                if (result > 0)
                {
                    return SuccessResp.Ok("Complete Order");
                }
                else
                {
                    return ErrorResp.InternalServerError("Incomplete Order");
                }
            }
            catch (Exception ex) { return ErrorResp.InternalServerError(ex.Message); }
        }

        public int CompleteOrder(Order order, User user)
        {
            order.Status = "Completed";

            _unitOfWork.Orders.Update(order);

            foreach (var t in order.OrderItems)
            {
                var perfume = t.Perfume;
                
                decimal perfumePrice = t.Price * t.Quantity;

                var transaction = new Transaction();
                transaction.TransactionId = Guid.NewGuid();
                transaction.OrderId = order.OrderId;
                transaction.PaymentMethod = order.PaymentMethod;
                transaction.PaymentStatus = "Completed";
                transaction.TransactionDate = DateTime.Now;

                _unitOfWork.Transactions.Create(transaction);
            }
            return _context.SaveChanges();
        }
    }
}
