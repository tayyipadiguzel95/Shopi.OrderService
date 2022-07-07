using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Business.Orders.Extension;
using OrderService.Domain.Data.EntityFramework;
using Shopi.OrderService.Integrations.Clients.UserService.Extensions;

namespace OrderService.Application.Common.Extensions;

public static class DependencyInjectionExtensions
{
    public static void Register(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<DataContext>();
        services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("TestDB"));
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


        #region RegisterServices

        services.RegisterOrderService();
        services.RegisterUserServiceClient(configuration);

        #endregion
    }
}