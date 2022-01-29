using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Project.Entities;

namespace Project.Extentions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ExtentedUserAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (ExtendedUser)context.HttpContext.Items["ExtendedUser"];
            if (user == null)
            {
                throw new UnauthorizedAccessException("invalid token");
            }
            else if (user.IsActive == false)
            {
                throw new UnauthorizedAccessException("user is banned");
            }
        }
    }
}