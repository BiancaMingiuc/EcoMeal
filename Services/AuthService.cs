using EcoMeal1.Constants;
using EcoMeal1.Controllers.Requests;
using EcoMeal1.Entities_CodeFirst;
using EcoMeal1.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using RegisterRequest = EcoMeal1.Controllers.Requests.RegisterRequest;

namespace EcoMeal1.Services
{
    public class AuthService(SignInManager<EcoMealUser> signIn, UserManager<EcoMealUser> userManager) : IAuthService
    {
        public async Task<SignInResult> LoginAsync(LoginModel request)
        {
            return await signIn.PasswordSignInAsync(request.Email, request.Password, isPersistent: true, lockoutOnFailure: false);
        }

        public async Task<IdentityResult> RegisterAsync(RegisterRequest request)
        {
            var user = new EcoMealUser
            {
                UserName = request.Email,
                Email = request.Email,
                FullName = request.FullName,
                EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, AppRoles.Customer);
            }

            return result;
        }

        public async Task<List<(EcoMealUser User, IList<string> Roles)>> GetAllUsersWithRolesAsync()
        {
            var users = userManager.Users.ToList();
            var result = new List<(EcoMealUser, IList<string>)>();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                result.Add((user, roles));
            }

            return result;
        }

        public async Task<IdentityResult> ChangeUserRoleAsync(string userId, string newRole)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            var currentRoles = await userManager.GetRolesAsync(user);
            var rolesToRemove = currentRoles
                .Where(r => r == AppRoles.Customer || r == AppRoles.BusinessManager)
                .ToList();

            await userManager.RemoveFromRolesAsync(user, rolesToRemove);

            return await userManager.AddToRoleAsync(user, newRole);
        }

    }
}

