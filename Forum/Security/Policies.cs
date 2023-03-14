using Microsoft.AspNetCore.Authorization;

namespace Forum.Security
{
    public static class Policies
    {
        public const string AdminOnly = "AdminOnly";
        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("UserTypeID", "250")
                .RequireClaim("UserTypeName", "Admin")
                .Build();
        }

        public const string ModeratorOnly = "ModeratorOnly";
        public static AuthorizationPolicy ModeratorPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("UserTypeID", "375")
                .RequireClaim("UserTypeName", "Moderator")
                .Build();
        }

        public const string OwnerOnly = "OwnerOnly";
        public static AuthorizationPolicy OwnerPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("UserTypeID", "535")
                .RequireClaim("UserTypeName", "Owner")
                .Build();
        }

    }
}
