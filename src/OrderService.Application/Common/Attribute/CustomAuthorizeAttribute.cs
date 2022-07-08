using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Infrastructure.Enums;
using OrderService.Infrastructure.Extensions;
using OrderService.Infrastructure.Public;
using OrderService.Infrastructure.Public.Result;
using Shopi.OrderService.Integrations.Clients.UserService.Interfaces;

namespace OrderService.Application.Common.Attribute;

public class CustomAuthorizeAttribute : ActionFilterAttribute
{
    private readonly AuthorizeMode AuthorizeMode;

    public CustomAuthorizeAttribute()
    {
    }

    public CustomAuthorizeAttribute(AuthorizeMode authorizeMode)
    {
        AuthorizeMode = authorizeMode;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        using var serviceScope = ServiceProviderFactory.GetScope();
        var rd = context.RouteData;
        var controllerName = rd.Values["controller"]?.ToString();
        var actionName = rd.Values["action"]?.ToString();

        var result = serviceScope.ServiceProvider.GetService<IAuthServiceClient>()!
            .CheckAuth(controllerName, actionName, AuthorizeMode).Result;

        switch (result.Result)
        {
            case HttpStatusCode.Unauthorized:
                context.Result = HttpStatusCode.Unauthorized.Result(ClientResultCode.Authorization,
                    "authorized.error");
                break;
            case HttpStatusCode.Forbidden:
                context.Result = HttpStatusCode.Forbidden.Result(ClientResultCode.Forbidden,
                    "forbidden.error");
                break;
            case HttpStatusCode.OK:
            default:
                break;
        }
    }
}