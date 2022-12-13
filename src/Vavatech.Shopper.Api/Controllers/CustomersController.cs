using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Vavatech.Shopper.Api.Controllers
{
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        [Authorize(Roles = "developer, trainer")]
        [HttpGet]
        public ActionResult<string> Get()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var email = this.User.FindFirstValue(ClaimTypes.Email);


            return Ok("Hello Customers");
        }

        // GET api/customers/adult
        [Authorize(Policy = "Adult")]
        [HttpGet("adult")]
        public string GetForAdult()
        {
            return "Hello Adult";
        }
    }
}
