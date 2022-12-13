using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Vavatech.Shopper.ClientApp
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "John Smith"),
            };

            var anonymous = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(anonymous);

            return Task.FromResult(new AuthenticationState(principal));
        }
    }
}
