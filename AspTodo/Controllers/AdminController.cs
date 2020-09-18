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

            var everyone = await _userManager.Users.ToArrayAsync();

            var users = everyone.Except(admins).ToArray();

            var viewModel = new AdminViewModel
            {
                Administrators = admins,
                Users = users,
            };

            return View(viewModel);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {           
            var user = await _userManager.FindByIdAsync(id.ToString());

            var results = _userManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }
    }
}
