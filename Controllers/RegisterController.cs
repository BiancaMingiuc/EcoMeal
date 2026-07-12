using EcoMeal1.Services.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EcoMeal1.Controllers
{
    [ApiController]
    [Route("/")]
    public class RegisterController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Login request, [FromQuery] string? returnUrl)
        {
            var result = await authService.LoginAsync(request);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl ?? "/");
            }
            return LocalRedirect($"/account/login?error=Invalid login&returnUrl={returnUrl}");
        }
    }
}
