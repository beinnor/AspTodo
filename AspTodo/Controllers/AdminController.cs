using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspTodo.Data;
using AspTodo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspTodo.Controllers
{
    [Authorize(Roles = Constants.AdministratorRole)]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var admins = (await _userManager.GetUsersInRoleAsync(Constants.AdministratorRole)).ToArray();

            var users = await _userManager.GetUsersInRoleAsync(Constants.UserRole)).ToArray();

            var viewModel = new AdminViewModel
            {
                Administrators = admins,
                Users = users,
            };

            return View(viewModel);

        }
    }
}
