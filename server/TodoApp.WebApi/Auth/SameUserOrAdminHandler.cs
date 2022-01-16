using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TodoApp.WebApi.Auth
{
    public class SameUserOrAdminHandler : AuthorizationHandler<SameUserOrAdminRequirement, int>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       SameUserOrAdminRequirement requirement,
                                                       int userId)
        {
            if (context.User.FindFirst(ClaimTypes.Role)?.Value == "admin")
            {
                context.Succeed(requirement);
            }

            var currentUserId = int.Parse(context.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (currentUserId == userId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class SameUserOrAdminRequirement : IAuthorizationRequirement { }
}
