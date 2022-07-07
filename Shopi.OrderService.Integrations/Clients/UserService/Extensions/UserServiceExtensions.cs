using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shopi.OrderService.Integrations.Clients.UserService.Services;
using Shopi.OrderService.Integrations.Clients.UserService.Interfaces;
using Shopi.OrderService.Integrations.Clients.UserService.Configuration;

namespace Shopi.OrderService.Integrations.Clients.UserService.Extensions;

public static class UserServiceExtensions
{
    /// <summary>
    /// RegisterUserServiceClient
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void RegisterUserServiceClient(this IServiceCollection services,IConfiguration configuration )
    {
        services.AddTransient<IAuthServiceClient, AuthServiceClient>();
        services.AddTransient<IUserServiceClient, UserServiceClient>();

        services.AddSingleton(configuration.GetSection("Configurations:UserServiceConfiguration").Get<UserServiceConfiguration>());
        
    }
}