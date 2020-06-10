using Microsoft.AspNetCore.Mvc;
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
    }
}