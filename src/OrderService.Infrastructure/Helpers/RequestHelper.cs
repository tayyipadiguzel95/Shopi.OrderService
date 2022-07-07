using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Infrastructure.Enums;
using OrderService.Infrastructure.Public;
using OrderService.Infrastructure.Public.Result;
using RestSharp;

namespace OrderService.Infrastructure.Helpers;

/// <summary>
/// RequestHelper
/// </summary>
public static class RequestHelper
{
    public static async Task<ServiceResponse<T>> SendRequestAsync<T>(string url, Method method,
        List<KeyValuePair<string, string>> paramList, object jsonBody = null,
        List<KeyValuePair<string, string>> headers = null, int timeout = 0)
    {
        var client = new RestClient(url);
        var request = new RestRequest(url, method);

        using var serviceScope = ServiceProviderFactory.GetScope();
        var httpContextAccessor = serviceScope.ServiceProvider.GetService<IHttpContextAccessor>();
        if (httpContextAccessor?.HttpContext != null)
        {
            var excludeHeaders = new[] {"Content-Length", "Content-Type"};
            var requestHeaders =
                httpContextAccessor.HttpContext.Request.Headers.Where(x => !excludeHeaders.Contains(x.Key));
            foreach (var (key, value) in requestHeaders)
                request.AddHeader(key, value);
        }

        if (headers != null)
        {
            foreach (var header in headers)
                request.AddHeader(header.Key, header.Value);
        }

        if (paramList != null)
            foreach (var param in paramList)
                request.AddQueryParameter(param.Key, param.Value);

        if (jsonBody != null)
            request.AddJsonBody(jsonBody);

        if (timeout > 0)
            request.Timeout = timeout;


        var handlerResponse = new HandlerResponse();
        var result = await client.ExecuteAsync(request);

        if (result.StatusCode != HttpStatusCode.OK)
        {
            switch (result.StatusCode)
            {
                case HttpStatusCode.NoContent:
                    return handlerResponse.ServiceResponse(default(T), ResultCode.NoContent,
                        JsonSerializer.Deserialize<ClientServiceResponse>(result.Content).Message);

                case HttpStatusCode.InternalServerError:
                    return handlerResponse.ServiceResponse(default(T), ResultCode.Exception,
                        JsonSerializer.Deserialize<ClientServiceResponse>(result.Content)?.Message);

                case HttpStatusCode.Unauthorized:
                    return handlerResponse.ServiceResponse(default(T), ResultCode.AuthorizationError,
                        JsonSerializer.Deserialize<ClientServiceResponse>(result.Content)?.Message);

                case HttpStatusCode.BadRequest:
                    var response400 = JsonSerializer.Deserialize<ClientServiceResponse>(result.Content);
                    return response400?.TypeCode == ClientResultCode.Validation
                        ? handlerResponse.ServiceResponse(default(T), ResultCode.ValidationError,
                            response400.Message)
                        : handlerResponse.ServiceResponse(default(T), ResultCode.Warning, response400?.Message);
                case HttpStatusCode.NonAuthoritativeInformation:
                    return handlerResponse.ServiceResponse(default(T), ResultCode.OtpVerification,
                        JsonSerializer.Deserialize<ClientServiceResponse>(result.Content)?.Message);
                case HttpStatusCode.PaymentRequired:
                    return handlerResponse.ServiceResponse(default(T), ResultCode.PaymentError,
                        JsonSerializer.Deserialize<ClientServiceResponse>(result.Content)?.Message);
                default:
                    return handlerResponse.ServiceResponse(default(T), ResultCode.Warning,
                        JsonSerializer.Deserialize<ClientServiceResponse>(result.Content)?.Message);
            }
        }

        var response = JsonSerializer.Deserialize<T>(result.Content);
        return handlerResponse.ServiceResponse(response);
    }
}