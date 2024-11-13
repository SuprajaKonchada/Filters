using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ContentManagement.Filters;

public class RoleBasedAuthorizationFilter : IAuthorizationFilter
{
    /// <summary>
    /// Executes authorization logic to check if the user has the required role to access the resource.
    /// </summary>
    /// <param name="context">The context of the authorization request.</param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Get the current user from the HTTP context
        var user = context.HttpContext.User;

        // Check if the user is in either the "Admin" or "User" role
        if (!user.IsInRole("Admin") && !user.IsInRole("User"))
        {
            // If the user doesn't have the required roles, return a Forbidden result
            context.Result = new ForbidResult();
        }
    }
}
