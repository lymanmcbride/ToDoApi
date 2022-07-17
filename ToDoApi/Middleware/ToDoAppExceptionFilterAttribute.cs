using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ToDoApi.Middleware
{
    public class ToDoAppExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = new JsonResult(new
            { message = context.Exception.Message,
                context.Exception.StackTrace })
            {
                StatusCode = 500
            };
        }
    }
}