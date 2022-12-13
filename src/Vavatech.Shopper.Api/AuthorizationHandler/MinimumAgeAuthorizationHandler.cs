using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Vavatech.Shopper.Api.Authorization;

namespace Vavatech.Shopper.Api.AuthorizationHandler;

public class MinimumAgeAuthorizationHandler : AuthorizationHandler<MinimumAgeRequirment>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        MinimumAgeRequirment requirement)
    {
        var dateOfBirth = DateTime.Parse( context.User.FindFirstValue(ClaimTypes.DateOfBirth));

        var age = DateTime.Now.Year - dateOfBirth.Year;

        if (age >= requirement.MinimumAge)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;  
    }
}
