using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.UI.AuthorizationRequirements
{
    public class AccountAccessRequirement : IAuthorizationRequirement
    {
        public AccountAccessRequirement(string accountUserName)
        {
            AccountUserName = accountUserName;
        }

        public string AccountUserName { get; set; }
    }

    public class AccountAccessHandler : AuthorizationHandler<AccountAccessRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            AccountAccessRequirement requirement)
        {
            var user = context.User;

            if (isAccountUser(user, requirement.AccountUserName) || isAdmin(user))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        public bool isAccountUser(ClaimsPrincipal user, string accountUserName)
            => user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name && x.Value == accountUserName) != null;

        public bool isAdmin(ClaimsPrincipal user)
            => user.IsInRole("Admin");
    }

    public static class AuthorizationPolicyBuilderExtensions
    {
        public static AuthorizationPolicyBuilder RequireAccountAccess(
            this AuthorizationPolicyBuilder builder,
            string accountUserName)
        {
            builder.AddRequirements(new AccountAccessRequirement(accountUserName));
            return builder;
        }
    }
}
