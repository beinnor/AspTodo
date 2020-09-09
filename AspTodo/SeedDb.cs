using AspTodo.Data;
using AspTodo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspTodo
{
    public class SeedDb
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            await AddRolesAsync(serviceProvider);
            await AddDefaultAdminUserAsync(serviceProvider);            
        }

        private static async Task AddRolesAsync(IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var alreadyExists = await roleManager
                .RoleExistsAsync(Constants.AdministratorRole);

            if (alreadyExists) return;

            await roleManager.CreateAsync(new IdentityRole(Constants.AdministratorRole));
        }

        private static async Task AddDefaultAdminUserAsync(IServiceProvider serviceProvider)
        {
            UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var defaultAdmin = await userManager.Users
                .Where(x => x.UserName == "admin@asptodo.local").SingleOrDefaultAsync();

            if (defaultAdmin != null) return;

            defaultAdmin = new IdentityUser
            {
                UserName = "admin@asptodo.local",
                Email = "admin@asptodo.local",
            };

            await userManager.CreateAsync(defaultAdmin, "password");
            await userManager.AddToRoleAsync(defaultAdmin, Constants.AdministratorRole);
        }
        
    }
}
