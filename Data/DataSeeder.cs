using Microsoft.AspNetCore.Identity;

namespace HospitalQueueMS.Data
{
    public static class DataSeeder
    {
        public static async Task SeedUsersAndRolesAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Seed Roles
            string[] roles = { "Admin", "Doctor", "Reception" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Seed Admin
            if (await userManager.FindByNameAsync("admin") == null)
            {
                var adminUser = new IdentityUser { UserName = "admin" };
                await userManager.CreateAsync(adminUser, "123");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // Seed Doctor
            if (await userManager.FindByNameAsync("doctor") == null)
            {
                var doctorUser = new IdentityUser { UserName = "doctor" };
                await userManager.CreateAsync(doctorUser, "123");
                await userManager.AddToRoleAsync(doctorUser, "Doctor");
            }

            // Seed Reception
            if (await userManager.FindByNameAsync("reception") == null)
            {
                var receptionUser = new IdentityUser { UserName = "reception" };
                await userManager.CreateAsync(receptionUser, "123");
                await userManager.AddToRoleAsync(receptionUser, "Reception");
            }
        }
    }
}
