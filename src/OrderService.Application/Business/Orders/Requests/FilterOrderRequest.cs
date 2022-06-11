using OrderService.Domain.Enums;
using OrderService.Infrastructure.Dtos;

namespace OrderService.Application.Business.Orders.Requests;

/// <summary>
/// FilterOrderRequest
/// </summary>
public class FilterOrderRequest : PagingBaseDto
{
    public FilterOrderRequest()
    {
        Statuses = new List<OrderStatus>();
    }
    public string SearchText { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<OrderStatus> Statuses { get; set; }
}