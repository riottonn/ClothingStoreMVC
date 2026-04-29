using ClothingStore.Domain;
using Microsoft.AspNetCore.Identity;

namespace ClothingStoreMVC
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "Qwerty_1"; // Пароль має бути складним!

            if (await roleManager.FindByNameAsync("admin") == null)
                await roleManager.CreateAsync(new IdentityRole("admin")); // [cite: 422]

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded) await userManager.AddToRoleAsync(admin, "admin"); // [cite: 434]
            }
        }
    }
}