using EcoMeal1.Controllers.Requests;
using EcoMeal1.Entities_CodeFirst;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using RegisterRequest = EcoMeal1.Controllers.Requests.RegisterRequest;

namespace EcoMeal1.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<SignInResult> LoginAsync(LoginModel request);
        public Task<IdentityResult> RegisterAsync(RegisterRequest request);
        public Task<List<(EcoMealUser User, IList<string> Roles)>> GetAllUsersWithRolesAsync();
        public Task<IdentityResult> ChangeUserRoleAsync(string userId, string newRole);
    }
}
