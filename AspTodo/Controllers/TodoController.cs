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

            TodoItemsVm viewModel = new TodoItemsVm
            {
                TodoListId = Id,
                TodoItems = todoItems,
            };

            return View(viewModel);
        }

        public IActionResult Create(Guid Id)
        {
            TodoItem viewModel = new TodoItem()
            {
                TodoListId = Id,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descripton, IsComplete, TodoListId")] TodoItem todoItem)
        {
            IdentityUser currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            bool success = await _aspTodoService.AddTodoItemAsync(currentUser, todoItem);
            if (!success)
            {
                return BadRequest("Could not add todoitem.");
            }

            return RedirectToAction("Index", new { Id = todoItem.TodoListId});

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid ItemId, Guid ListId)
        {
            IdentityUser currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();


            if (!await _aspTodoService.DeleteTodoItemAsync(currentUser, ItemId))
            {
                return BadRequest("Could not delete item!");
            }

            return RedirectToAction("Index", new { Id = ListId });
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid ItemId, Guid ListId)
        {            
            IdentityUser currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            bool successful = await _aspTodoService.MarkItemDoneAsync(ItemId, ListId);
            if (!successful)
            {
                return BadRequest("Couldn't mark item as done!");
            }

            return RedirectToAction("Index", new { Id = ListId });
        }

    }
}
