using Microsoft.AspNetCore.Mvc;
using Services.Customer;
using ViewModel;

namespace Api.Controllers
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService identityService;

        public IdentityController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        [HttpPost]
        [Route("api/token")]        
        public IActionResult Authenticate(IdentityRequestViewModel model)
        {
            var response = identityService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

    }
}
