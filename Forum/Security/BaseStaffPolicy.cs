using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Forum.Security
{
    public class BaseStaffPolicyRequirement : IAuthorizationRequirement
    {
    }

    public class BaseStaffPolicyHandler : AuthorizationHandler<BaseStaffPolicyRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, BaseStaffPolicyRequirement requirement)
        {
            var isModerator = context.User.FindFirst(c => c.Type == ClaimTypes.Role && c.Value == "Moderator");
            var isAdmin = context.User.FindFirst(c=>c.Type == ClaimTypes.Role && c.Value == "Admin");
            var isOwner = context.User.FindFirst(c => c.Type == ClaimTypes.Role && c.Value == "Owner");

            if (isModerator is null && isAdmin is null && isOwner is null)
            {
                await Task.CompletedTask;
            }
            else
            {
                context.Succeed(requirement);
            }

            await Task.CompletedTask;

        }
    }
}
