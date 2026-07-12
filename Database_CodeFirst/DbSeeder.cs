using EcoMeal1.Constants;
using EcoMeal1.Entities_CodeFirst;
using Microsoft.AspNetCore.Identity;

namespace EcoMeal1.Database_CodeFirst
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider services, IConfiguration configuration)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<EcoMealUser>>();

            foreach (var role in AppRoles.AllRoles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var adminEmail = configuration["SeedAdmin:Email"];
            var adminPassword = configuration["SeedAdmin:Password"];

            if (string.IsNullOrEmpty(adminEmail) || string.IsNullOrEmpty(adminPassword))
            {
                return;
            }

            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
            if (existingAdmin != null)
            {
                return;
            }

            var admin = new EcoMealUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                FullName = "Admin"
            };

            var result = await userManager.CreateAsync(admin, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRolesAsync(admin, AppRoles.AllRoles);
            }
        }
    }
}
