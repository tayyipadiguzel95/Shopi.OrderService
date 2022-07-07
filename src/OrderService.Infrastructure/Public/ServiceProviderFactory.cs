using Microsoft.Extensions.DependencyInjection;

namespace OrderService.Infrastructure.Public;

public class ServiceProviderFactory
{
    internal static IServiceProvider ServiceProvider { get; set; }


    /// <summary>
    /// Configure ServiceActivator with full serviceProvider
    /// </summary>
    /// <param name="serviceProvider"></param>
    public static void Configure(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }
    
    /// <summary>
    /// Create a scope where use this ServiceActivator
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IServiceScope GetScope(IServiceProvider serviceProvider = null)
    {
        var provider = serviceProvider ?? ServiceProvider;
        return provider?
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
    }
    
}