using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using ToDoApi.Repository.Exceptions;

namespace ToDoApi.Middleware
{
    public class ToDoListsExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ToDoDatabaseAccessException toDoDatabaseAccessException)
            {
                context.Result = new JsonResult(new { message = toDoDatabaseAccessException.Message })
                {
                    StatusCode = 500
                };
            }
        }
    }
}