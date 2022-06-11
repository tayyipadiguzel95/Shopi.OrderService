using OrderService.Application.Business.Orders.Requests;
using OrderService.Application.Business.Orders.Responses;
using OrderService.Infrastructure.Public.Result;

namespace OrderService.Application.Business.Orders.Interfaces;

public interface IOrderService  
{
    Task<ServiceResponse<CreateOrderResponse>> CreateOrdersAsync(List<CreateOrderRequest> orders);
    Task<ServiceResponsePagination<FilterOrderResponse>> FilterOrdersAsync(FilterOrderRequest request);

}