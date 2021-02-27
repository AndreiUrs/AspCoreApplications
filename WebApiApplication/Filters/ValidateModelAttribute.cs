using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiApplication.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
  
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.ModelState.IsValid is false)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
           
        }
    }
}
