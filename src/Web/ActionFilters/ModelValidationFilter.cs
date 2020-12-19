using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tss.Core.Constants;

namespace Tss.Web.ActionFilters
{
    /// <summary>
    /// Intriduces Model state auto validation to reduce code duplication
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute" />
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Validates Model automaticaly 
        /// </summary>
        /// <param name="context"></param>
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var status = (int)HttpStatusCode.BadRequest;
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Instance = Guid.NewGuid().ToString(),
                    Status = status,
                    Type = $"https://httpstatuses.com/{status}",
                    Detail = HttpMessage.InputValidationError
                };


                context.Result = new BadRequestObjectResult(problemDetails)
                {
                    ContentTypes =
                    {
                        HttpContentType.ProblemJson,
                    }
                };
            }
        }
    }
}
