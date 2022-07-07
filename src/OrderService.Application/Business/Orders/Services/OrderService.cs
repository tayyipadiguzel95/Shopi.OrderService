using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Business.Orders.Interfaces;
using OrderService.Application.Business.Orders.Requests;
using OrderService.Application.Business.Orders.Responses;
using OrderService.Domain.Data.EntityFramework;
using OrderService.Domain.Data.EntityFramework.Entities.Orders;
using OrderService.Infrastructure.Enums;
using OrderService.Infrastructure.Extensions;
using OrderService.Infrastructure.Public;
using OrderService.Infrastructure.Public.Result;

namespace OrderService.Application.Business.Orders.Services;

/// <summary>
/// OrderService
/// </summary>
public class OrderService : HandlerResponse, IOrderService
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public OrderService(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    /// <summary>
    /// CreateOrdersAsync
    /// </summary>
    /// <param name="orders"></param>
    /// <returns></returns>
    public async Task<ServiceResponse<CreateOrderResponse>> CreateOrdersAsync(List<CreateOrderRequest> orders)
    {
        var excludedOrders = orders.Where(a => a.BrandId == default);

        var entities = _mapper.Map<List<Order>>(orders.Where(a => a.BrandId > 0));

        if (!entities.Any())
            return NoContent<CreateOrderResponse>("No one record has BrandId");

        await _dataContext.Orders.AddRangeAsync(entities);
        await _dataContext.SaveChangesAsync();

        var response = new CreateOrderResponse()
        {
            Ids = entities.Select(a => a.Id).ToList(),
            UnCreatedOrdersList = _mapper.Map<List<UnCreatedOrder>>(excludedOrders)
        };

        return ServiceResponse(response);
    }

    public async Task<ServiceResponsePagination<FilterOrderResponse>> FilterOrdersAsync(
        FilterOrderRequest filterOrderRequest)
    {
        var query = Filter(filterOrderRequest);

        var response = await query.ProjectTo<FilterOrderResponse>(_mapper.ConfigurationProvider)
            .Sort(filterOrderRequest)
            .Paging(filterOrderRequest, out var totalCount)
            .ToListAsync();

        return ServiceResponsePagination(response,
            new PaginationDto(totalCount, filterOrderRequest.PageNumber, filterOrderRequest.PageSize));
    }

    private IQueryable<Order> Filter(FilterOrderRequest filterOrderRequest)
    {
        var query = _dataContext.Orders.AsQueryable();

        if (!string.IsNullOrEmpty(filterOrderRequest.SearchText))
            // We can use ILike
            query = query.Where(a =>
                EF.Functions.Like(a.CustomerName, $"{filterOrderRequest.SearchText}") ||
                EF.Functions.Like(a.StoreName, $"{filterOrderRequest.SearchText}"));

        if (filterOrderRequest.StartDate.HasValue)
            query = query.Where(a => a.CreatedOn > filterOrderRequest.StartDate.Value);

        if (filterOrderRequest.EndDate.HasValue)
            query = query.Where(a => a.CreatedOn < filterOrderRequest.EndDate.Value);

        if (filterOrderRequest.Statuses != null && filterOrderRequest.Statuses.Any())
            query = query.Where(a => filterOrderRequest.Statuses.Contains(a.Status));

        return query;
    }
}