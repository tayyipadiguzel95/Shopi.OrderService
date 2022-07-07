using System.Net;
using OrderService.Infrastructure.Enums;
using OrderService.Infrastructure.Helpers;
using OrderService.Infrastructure.Public.Result;
using RestSharp;
using Shopi.OrderService.Integrations.Clients.UserService.Configuration;
using Shopi.OrderService.Integrations.Clients.UserService.Interfaces;

namespace Shopi.OrderService.Integrations.Clients.UserService.Services;

public class AuthServiceClient : IAuthServiceClient
{
    private readonly UserServiceConfiguration _userServiceConfiguration;

    public AuthServiceClient(UserServiceConfiguration userServiceConfiguration)
    {
        _userServiceConfiguration = userServiceConfiguration;
    }

    /// <summary>
    /// CheckAuth
    /// </summary>
    /// <param name="controllerName"></param>
    /// <param name="actionName"></param>
    /// <param name="authorizeMode"></param>
    /// <returns></returns>
    public async Task<ServiceResponse<HttpStatusCode>> CheckAuth(string controllerName, string actionName,
        AuthorizeMode authorizeMode)
    {
        var paramList = new List<KeyValuePair<string, string>>
        {
            new("controllerName", controllerName),
            new("actionName", actionName),
            new("authorizeMode", authorizeMode.ToString())
        };

        var response = await RequestHelper.SendRequestAsync<HttpStatusCode>(
            $"{_userServiceConfiguration.Endpoint}/api/Auth/CheckAuth", Method.Get, paramList);
        return response;
    }
}