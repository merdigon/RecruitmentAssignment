using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace RecruitmentAssignment.Authentication
{
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!context.HttpContext.Request.Headers.TryGetValue("ApiKey", out var providedApiKey))
            {
                context.Result = Unauthorized();
                return Task.CompletedTask;
            }

            //TODO Switch to more secured login method with tokens with limited lifespan
            var appSettingsService = (IAppSettingsHandler)context.HttpContext.RequestServices.GetService(typeof(IAppSettingsHandler));
            var validApiKey = appSettingsService.GetApiKey();

            if (!validApiKey.Equals(providedApiKey))
            {
                context.Result = Unauthorized();
                return Task.CompletedTask;
            }

            return next();
        }

        private IActionResult Unauthorized()
        {
            return new UnauthorizedResult();
        }
    }
}
