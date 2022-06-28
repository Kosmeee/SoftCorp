using Microsoft.AspNetCore.Authorization;
using Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Service.Auth.Attribute
{


    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : System.Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var user = (Account)context.HttpContext.Items["Account"];
            if (user != null) return;
            context.Result = new JsonResult(new { message = "Unauthorized" })
                { StatusCode = StatusCodes.Status401Unauthorized };
            context.HttpContext.Response.Headers["WWW-Authenticate"] = "Basic realm=\"\", charset=\"UTF-8\"";
        }
    }
}
