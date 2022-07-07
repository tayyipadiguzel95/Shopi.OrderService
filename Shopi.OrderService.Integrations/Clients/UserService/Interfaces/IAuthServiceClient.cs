using System.Net;
using OrderService.Infrastructure.Enums;
using OrderService.Infrastructure.Public.Result;

namespace Shopi.OrderService.Integrations.Clients.UserService.Interfaces;

public interface IAuthServiceClient
{
    /// <summary>
    /// CheckAuth
    /// </summary>
    /// <param name="controllerName"></param>
    /// <param name="actionName"></param>
    /// <param name="authorizeMode"></param>
    /// <returns></returns>
    Task<ServiceResponse<HttpStatusCode>> CheckAuth(string controllerName, string actionName, AuthorizeMode authorizeMode);
}