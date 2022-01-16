using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TodoApp.Shared.Interface;

namespace TodoApp.WebApi.Auth
{
    public class UserIsOwnerOrAdminHandler :  AuthorizationHandler<UserIsOwnerOrAdminRequirement, int>
    {
        private readonly ITodoItemService todoItemService;

        public UserIsOwnerOrAdminHandler(ITodoItemService todoItemService)
        {
            this.todoItemService = todoItemService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       UserIsOwnerOrAdminRequirement requirement,
                                                       int todoItemId)
        {
            if (context.User.FindFirst(ClaimTypes.Role)?.Value == "admin")
            {
                context.Succeed(requirement);
            }

            var todoItem = todoItemService.GetById(todoItemId);
            var userId = int.Parse(context.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (userId == todoItem.OwnerId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class UserIsOwnerOrAdminRequirement : IAuthorizationRequirement { }
}
