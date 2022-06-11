namespace OrderService.Application.Business.Orders.Responses;

/// <summary>
/// CreateOrderResponse
/// </summary>
public class CreateOrderResponse
{
    public List<int> Ids { get; set; }

    public List<UnCreatedOrder> UnCreatedOrdersList { get; set; }
}


/// <summary>
/// UnCreatedOrder
/// </summary>
public class UnCreatedOrder
{
    public decimal Price { get; set; }
    public string StoreName { get; set; }
    public string CustomerName { get; set; }
}