using Microsoft.AspNetCore.Mvc;
using OrderService.Infrastructure.Enums;
using OrderService.Infrastructure.Public.Result;

namespace OrderService.API.Controllers.Base;

/// <summary>
/// BaseController
/// </summary>
[Produces("application/json")]
[Route("api/[controller]/[action]")]
public abstract class BaseController : Controller
{
    /// <summary>
    /// Ok
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="response"></param>
    /// <returns></returns>
    [NonAction]
    protected virtual IActionResult Ok<T>(ServiceResponse<T> response)
    {
        switch (response.ResultCode)
        {
            case ResultCode.Success:
                return base.Ok(response.Result);
        }

        return HandleClientStatusCode(response);
    }


    /// <summary>
    /// HandleClientStatusCode
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    private IActionResult HandleClientStatusCode(ServiceResponse response)
    {
        return response.ResultCode switch
        {
            ResultCode.Success => base.Ok(response.Success),
            ResultCode.NoContent => StatusCode(StatusCodes.Status204NoContent,new ClientServiceResponse(ClientResultCode.Warning, response.Title, response.ResultMessage)),
            _ => StatusCode(StatusCodes.Status400BadRequest,
                new ClientServiceResponse(ClientResultCode.Warning, response.Title, response.ResultMessage))
        };
    }
}