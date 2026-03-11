using DealHub.Models.enums;
using Microsoft.AspNetCore.Identity;

namespace DealHub.Models
{
    public class IdentityConfig
    {
        public static async Task CreateAdminUserAsync(IServiceProvider provider)
        {
            var userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            string firstName = "Admin";
            string lastName = "Dealhub";
            string email = "admin@dealhub.cm";
            string password = "Sesame@123!";
            string roleName = RoleEnum.ADMIN.ToString();

            if(await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            if (await userManager.FindByNameAsync(email) == null)
            {
                ApplicationUser admin = new ApplicationUser { UserName = email, Email = email, FirstName = firstName, LastName = lastName, EmailConfirmed = true };
                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded) { 
                    await userManager.AddToRoleAsync(admin, roleName);
                }
            }
        }
    }
}
