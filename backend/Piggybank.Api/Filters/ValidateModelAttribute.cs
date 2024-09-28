using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

/// <summary>
/// Attribute that validates the model state before executing an action.
/// If the model state is invalid, it returns a Bad Request response with the validation errors.
/// </summary>
public class ValidateModelAttribute : ActionFilterAttribute
{
    /// <summary>
    /// Called before the action method is invoked.
    /// Validates the model state and returns a Bad Request result if the model is invalid.
    /// </summary>
    /// <param name="context">The context of the action executing.</param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
