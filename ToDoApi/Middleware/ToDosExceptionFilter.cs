using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToDoApi.Repository.Exceptions;

namespace ToDoApi.Middleware
{
    public class ToDosExceptionFilter : ToDoAppExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ToDoDatabaseAccessException or ListDatabaseAccessException)
            {
                context.Result = new JsonResult(new { message = context.Exception.Message })
                {
                    StatusCode = 500
                };
            }
            else
            {
                base.OnException(context);
            }
        }
    }
}
