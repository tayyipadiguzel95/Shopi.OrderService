using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using OrderService.Infrastructure.Extensions;

namespace OrderService.Infrastructure.Attributes;

public class HandleExceptionAttribute : ExceptionFilterAttribute
{
    
    public override void OnException(ExceptionContext context)
    {
        
         //log somewhere 
         context.Result = context.Exception.Result();
         base.OnException(context);

    }
}