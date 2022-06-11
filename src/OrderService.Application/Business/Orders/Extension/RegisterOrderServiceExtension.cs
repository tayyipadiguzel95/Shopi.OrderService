using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Business.Orders.Interfaces;

namespace OrderService.Application.Business.Orders.Extension;


public static class RegisterOrderServiceExtension
{

    /// <summary>
    /// RegisterOrderService
    /// </summary>
    /// <param name="services"></param>
    public static void RegisterOrderService(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, Services.OrderService>();
    }
}