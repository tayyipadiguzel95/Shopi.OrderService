using AutoMapper;
using OrderService.Application.Business.Orders.Requests;
using OrderService.Application.Business.Orders.Responses;
using OrderService.Domain.Data.EntityFramework.Entities.Orders;

namespace OrderService.Application.Business.Orders.Mapping;


/// <summary>
/// OrderMapping
/// </summary>
public class OrderMapping : Profile
{
    public OrderMapping()
    {
        CreateMap<CreateOrderRequest, Order>();
        CreateMap<CreateOrderResponse, Order>();
        CreateMap<CreateOrderRequest, UnCreatedOrder>();

        CreateMap<FilterOrderResponse, Order>().ReverseMap();


    }
}