using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace back_end.Filters;

public class ValidadeModelStateAtribute : ActionFilterAttribute
{
    // Método para validação do ModelState
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid == false)
        {
            context.Result = new BadRequestResult();
        }
    }
    
}