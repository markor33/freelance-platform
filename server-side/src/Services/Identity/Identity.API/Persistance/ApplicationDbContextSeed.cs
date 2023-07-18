using Identity.API.Constants;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Data
{
    public static class ApplicationDbContextSeed
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            await roleManager.CreateAsync(new IdentityRole<Guid>(Role.FREELANCER.ToString()));
            await roleManager.CreateAsync(new IdentityRole<Guid>(Role.CLIENT.ToString()));
            await roleManager.CreateAsync(new IdentityRole<Guid>("ADMIN"));

            var freelancer = new ApplicationUser()
            {
                Email = "freelancer@email.com",
                UserName = "freelancer",
            };
            await userManager.CreateAsync(freelancer, "12345");

            var client = new ApplicationUser()
            {
                Email = "client@email.com",
                UserName = "client",
            };
            await userManager.CreateAsync(client, "12345");

            var admin = new ApplicationUser()
            {
                Email = "admin@email.com",
                UserName = "admin",
            };
            await userManager.CreateAsync(admin, "12345");

            await userManager.AddToRoleAsync(freelancer, Role.FREELANCER.ToString());
            await userManager.AddToRoleAsync(client, Role.CLIENT.ToString());
            await userManager.AddToRoleAsync(admin, "ADMIN");
        }
    }
}
