using System.Collections.Generic;
using System.Linq;
using OrderService.Infrastructure.Enums;

namespace OrderService.Infrastructure.Public.Result
{
    /// <summary>
    /// ServiceResult
    /// </summary>
    public class ServiceResult
    {

        private readonly ServiceResponse _generic;

        public ServiceResult(ResultCode resultCode, string message)
        {
            _generic = new ServiceResponse
            {
                ResultCode = resultCode,
                ResultMessage = message
            };
        }
        
        public ServiceResult(ResultCode resultCode, string message, string title)
        {
            _generic = new ServiceResponse
            {
                ResultCode = resultCode,
                ResultMessage = message,
                Title = title
            };
        }

        public ServiceResponse ExecuteResult()
        {
            return _generic;
        }
    }
    /// <summary>
    /// ServiceResult
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResult<T>
    {

        private readonly ServiceResponse<T> _generic;

        public ServiceResult(T data, ResultCode resultCode, string message)
        {
            _generic = new ServiceResponse<T>
            {
                Result = data,
                ResultCode = resultCode,
                ResultMessage = message,
                Show = resultCode != ResultCode.Success
            };
        }
        
        public ServiceResult(T data, ResultCode resultCode, string message, string title)
        {
            _generic = new ServiceResponse<T>
            {
                Title = title,
                Result = data,
                ResultCode = resultCode,
                ResultMessage = message,
                Show = resultCode != ResultCode.Success
            };
        }

        public ServiceResponse<T> ExecuteResult()
        {
            return _generic;
        }
    }

    /// <summary>
    /// ServiceResultList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResultList<T>
    {

        private readonly ServiceResponseList<T> _generic;

        public ServiceResultList(IList<T> data, ResultCode resultCode, string message)
        {
            data ??= new List<T>();
            data = data.ToList();
            _generic = new ServiceResponseList<T>
            {
                Result = data,
                ResultCode = resultCode,
                ResultMessage = message
            };
            //if (!_generic.Any)
            //    _generic.ResultMessage = "";
        }
        public ServiceResponseList<T> ExecuteResult()
        {
            return _generic;
        }
    }
}
