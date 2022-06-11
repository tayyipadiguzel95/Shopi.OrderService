using Microsoft.AspNetCore.Mvc;
using OrderService.API.Controllers.Base;
using OrderService.Application.Business.Orders.Interfaces;
using OrderService.Application.Business.Orders.Requests;

namespace OrderService.API.Controllers;

/// <summary>
/// OrderController
/// </summary>
public class OrderController : BaseController
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// CreateOrders
    /// </summary>
    /// <param name="orders"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateOrdersAsync([FromBody] List<CreateOrderRequest> orders)
    {
        var response = await _orderService.CreateOrdersAsync(orders);
        return Ok(response);
    }


    /// <summary>
    /// FilterOrderRequest
    /// </summary>
    /// <param name="filterOrderRequest"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> FilterOrdersAsync([FromBody] FilterOrderRequest filterOrderRequest)
    {
        var response = await _orderService.FilterOrdersAsync(filterOrderRequest);
        return Ok(response);
    }
}