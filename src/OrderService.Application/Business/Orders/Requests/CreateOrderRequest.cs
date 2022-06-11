using OrderService.Domain.Enums;

namespace OrderService.Application.Business.Orders.Requests;

public class CreateOrderRequest
{
    public int BrandId { get; set; }
    public decimal Price { get; set; }
    public string StoreName { get; set; }
    public string CustomerName { get; set; }
    public OrderStatus Status { get; set; }
}