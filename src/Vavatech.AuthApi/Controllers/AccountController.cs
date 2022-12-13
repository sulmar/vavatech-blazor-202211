using Microsoft.AspNetCore.Mvc;
using Vavatech.AuthApi.Domain;
using Vavatech.AuthApi.Models;

namespace Vavatech.AuthApi.Controllers;

public class AccountController : ControllerBase
{

    // POST api/token
    // { "login": "john", "password": "123" }

    [HttpPost("api/token")]
    public async Task<ActionResult<string>> CreateTokenAsync(
        [FromBody] LoginViewModel model,
        [FromServices] IAuthService authService,
        [FromServices] ITokenService tokenService
        )
    {
        if (ModelState.IsValid)
        {
            if (authService.TryAuthorize(model.Login, model.Password, out User user))
            {
                var token = tokenService.Create(user);

                var cookieOptions = new CookieOptions
                {
                    SameSite = SameSiteMode.Strict,
                    Secure = true,
                    HttpOnly = true,
                };

                Response.Cookies.Append("X-Access-Token", token, cookieOptions);

                return Ok(token);
            }

        }

        return Unauthorized();
    }


}
