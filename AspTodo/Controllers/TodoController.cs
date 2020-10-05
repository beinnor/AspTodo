using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspTodo.Data;
using AspTodo.Models;
using AspTodo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspTodo.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {                
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAspTodoService _aspTodoService;

        public TodoController(UserManager<IdentityUser> userManager, IAspTodoService aspTodoService)
        {            
            _userManager = userManager;
            _aspTodoService = aspTodoService;
        }

        public async Task<IActionResult> Index(Guid Id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            List<TodoItem> todoItems = await _aspTodoService.GetTodoItemsAsync(currentUser, Id);
                        
            return View(todoItems);
        }

        
    }
}
