using cine_hub_server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace cine_hub_server.Data_access
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            // Перевірка ролей та їх додавання, якщо необхідно
            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    var role = new IdentityRole(roleName);
                    await roleManager.CreateAsync(role);
                }
            }
            // Перевірка, чи є в базі хоч один користувач з роллю "Admin"
            var adminUsers = await userManager.GetUsersInRoleAsync("Admin");
            bool adminExists = adminUsers.Any();
            if (!adminExists)
            {
                var adminUser = new User
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    Name = "Admin",
                    Surname = "User",
                    Birthday = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123"); // Надійний пароль

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
