using Microsoft.AspNetCore.Authorization;

namespace Vavatech.Shopper.Api.Authorization;

public class MinimumAgeRequirment : IAuthorizationRequirement
{
    public byte MinimumAge { get; set; }

    public MinimumAgeRequirment(byte minimumAge)
    {
        MinimumAge = minimumAge;
    }
}
