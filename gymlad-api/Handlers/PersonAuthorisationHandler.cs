using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

using GymLad.Models;

namespace GymLad.Handlers
{
    public class PersonAuthorisationHandler : AuthorizationHandler<SamePersonRequirement, Person>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SamePersonRequirement requirement, Person resource)
        {
            if (context.User.Identity?.Name == resource.UserName)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
    public class SamePersonRequirement : IAuthorizationRequirement { }
}