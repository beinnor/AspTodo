using AspTodo.Data;
using AspTodo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
            // ApplicationDbContext _context = serviceProvider.GetRequiredService<ApplicationDbContext>();


            var defaultAdmin = await userManager.Users
                .Where(x => x.UserName == "admin@asptodo.local").SingleOrDefaultAsync();

            if (defaultAdmin != null) return;

            defaultAdmin = new IdentityUser
            {
                UserName = "admin@asptodo.local",
                Email = "admin@asptodo.local",
                EmailConfirmed = true,
            };

            await userManager.CreateAsync(defaultAdmin, "password");            
            await userManager.AddToRoleAsync(defaultAdmin, Constants.AdministratorRole);


            //TodoList todoList1 = new TodoList
            //{
            //    Id = new Guid(),
            //    Name = "DefaultAdminList1",
            //    UserId = defaultAdmin.Id,
            //    User = defaultAdmin,   
            //    TodoItems = {},
            //};

            //TodoItem todoItem1 = new TodoItem
            //{
            //    Id = new Guid(),
            //    Descripton = "todoitem1",
            //    IsComplete = false,
                
            //    TodoListId = todoList1.Id,
            //    TodoList = todoList1
            //};

            //TodoItem todoItem2 = new TodoItem
            //{
            //    Id = new Guid(),
            //    Descripton = "todoitem2",
            //    IsComplete = true,

            //    TodoListId = todoList1.Id,
            //    TodoList = todoList1
            //};



           // todoList1.TodoItems.Add(todoItem1);
            

           // _context.Add(todoList1);

            //_context.Add(todoItem1);
            //_context.Add(todoItem2);

            // await _context.SaveChangesAsync();
        }

        
    }
}
