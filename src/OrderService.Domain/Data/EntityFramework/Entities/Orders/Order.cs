using OrderService.Domain.Enums;

namespace OrderService.Domain.Data.EntityFramework.Entities.Orders;

public class Order : BaseEntity
{
    public int BrandId { get; set; }
    public decimal Price { get; set; }
    public string StoreName { get; set; }
    public string CustomerName { get; set; }
    public OrderStatus Status { get; set; }
}