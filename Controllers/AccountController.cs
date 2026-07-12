using EcoMeal1.Entities_CodeFirst;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcoMeal1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<EcoMealUser> _userManager;

        public AccountController(UserManager<EcoMealUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<string> GetCurrentUserId()
        {
            EcoMealUser usr = await GetCurrentUserAsync();
            return usr?.Id;
        }

        private Task<EcoMealUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
