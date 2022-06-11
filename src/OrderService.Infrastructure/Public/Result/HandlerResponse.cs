using OrderService.Infrastructure.Enums;

namespace OrderService.Infrastructure.Public.Result;

/// <summary>
/// HandlerResponse
/// </summary>
public class HandlerResponse
{
    public ServiceResponse ServiceResponse()
    {
        return ServiceResponse(ResultCode.Success, "");
    }

    public ServiceResponse ServiceResponse(ResultCode resultCode, string message)
    {
        var sr = new ServiceResult(resultCode, message);
        return sr.ExecuteResult();
    }

    public ServiceResponse<T> ServiceResponse<T>(T data, ResultCode resultCode = ResultCode.Success,
        string message = "", string title = "", bool dontOverrideResult = false)
    {
        if (data == null && !dontOverrideResult)
        {
            resultCode = ResultCode.NoContent;
            message = "";
        }

        var sr = new ServiceResult<T>(data, resultCode, message, title);
        return sr.ExecuteResult();
    }

    public ServiceResponse<T> NoContent<T>(string message)
    {
        var sr = new ServiceResult<T>(default(T), ResultCode.NoContent, message);
        return sr.ExecuteResult();
    }

    public ServiceResponsePagination<T> ServiceResponsePagination<T>(IList<T> data, PaginationDto page,
        ResultCode resultCode = ResultCode.Success, string message = "")
    {
        data = data.ToList();
        return new ServiceResponsePagination<T>
        {
            Result = new PaginationList<T>(page, data),
            ResultCode = resultCode,
            ResultMessage = message
        };
    }
}