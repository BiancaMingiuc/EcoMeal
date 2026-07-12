using EcoMeal1.Controllers.Requests;
using EcoMeal1.Entities_CodeFirst;
using EcoMeal1.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegisterRequest = EcoMeal1.Controllers.Requests.RegisterRequest;

namespace EcoMeal1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController(IAuthService authService, SignInManager<EcoMealUser> signInManager) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginModel request, [FromQuery] string? returnUrl)
        {
            var result = await authService.LoginAsync(request);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl ?? "/");
            }
            return LocalRedirect($"/account/login?error=Invalid login&returnUrl={returnUrl}");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request, [FromQuery] string? returnUrl)
        {
            if (request.Password != request.ConfirmPassword)
            {
                return LocalRedirect("/account/register?error=Passwords do not match.");
            }

            var result = await authService.RegisterAsync(request);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl ?? "/account/login");
            }

            var firstError = result.Errors.FirstOrDefault()?.Description ?? "Registration failed.";
            return LocalRedirect($"/account/register?error={Uri.EscapeDataString(firstError)}");
        }

        [HttpPost("logout")]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return LocalRedirect("/account/login");
        }
    }
}
