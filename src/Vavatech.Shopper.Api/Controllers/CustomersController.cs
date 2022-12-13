using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vavatech.Shopper.Api.Controllers
{
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public ActionResult<string> Get()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
                
            return Ok("Hello Customers");
        }
    }
}
