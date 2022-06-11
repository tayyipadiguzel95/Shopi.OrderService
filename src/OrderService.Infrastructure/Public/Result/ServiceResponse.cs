using OrderService.Infrastructure.Enums;

namespace OrderService.Infrastructure.Public.Result;

/// <summary>
/// ServiceResponse
/// </summary>
public class ServiceResponse
{
    public ResultCode ResultCode { get; set; }
    public string Title { get; set; }
    public string ResultMessage { get; set; }
    public bool Show { get; set; }
    public bool Success => ResultCode == ResultCode.Success;
}

/// <summary>
/// ServiceResponse
/// </summary>
/// <typeparam name="T"></typeparam>
public class ServiceResponse<T> : ServiceResponse
{
    public T Result { get; set; }
}

/// <summary>
/// ServiceResponseList
/// </summary>
/// <typeparam name="T"></typeparam>
public class ServiceResponseList<T> : ServiceResponse
{
    public ServiceResponseList()
    {
        Result = new List<T>();
    }

    public IList<T> Result { get; set; }
}

/// <summary>
/// ServiceResponsePagination
/// </summary>
/// <typeparam name="T"></typeparam>
public class ServiceResponsePagination<T> : ServiceResponse
{
    public PaginationList<T> Result { get; set; }
}