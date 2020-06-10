using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Contracts.Requests;
using Store.Services;

namespace Store.Controllers
{
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        // POST /api/v1/identity/register
        public async Task<ActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password);
        }
    }
}