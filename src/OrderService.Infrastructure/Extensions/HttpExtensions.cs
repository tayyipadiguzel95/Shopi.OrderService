using System.Net;
using Microsoft.AspNetCore.Mvc;
using OrderService.Infrastructure.Public.Result;

namespace OrderService.Infrastructure.Extensions;

/// <summary>
/// HttpExtensions
/// </summary>
public static class HttpExtensions
{
    /// <summary>
    /// Result
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public static IActionResult Result(this Exception exception)
    {
        var message = EnvironmentExtensions.IsProduction ? "internal server error" : exception.Message;
        return new JsonResult(new ClientServiceResponse(ClientResultCode.Error, null, message))
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
    }
    
    
    /// <summary>
    /// Result
    /// </summary>
    /// <param name="statusCode"></param>
    /// <param name="clientResultCode"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static IActionResult Result(this HttpStatusCode statusCode, ClientResultCode clientResultCode, string message)
    {
        return new JsonResult(new ClientServiceResponse(clientResultCode, null, message))
        {
            StatusCode = (int)statusCode
        };
    }
    
}