using AspTodo.Models;
using AspTodo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspTodo.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;        
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAspTodoService _aspTodoService;

        public HomeController(ILogger<HomeController> logger,                              
                              UserManager<IdentityUser> userManager,
                              IAspTodoService aspTodoService)
        {
            _logger = logger;            
            _userManager = userManager;
            _aspTodoService = aspTodoService;
        }

        // GET: Todo
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            List<TodoList> lists = await _aspTodoService.GetTodoListsAsync(currentUser);           

            return View(lists);

        }

        // GET: Todo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TodoList todoList)
        {
            IdentityUser currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            if (!ModelState.IsValid)
            {                
                return RedirectToAction(nameof(Index));
            }

            bool success = await _aspTodoService.AddListAsync(currentUser, todoList);
            if (!success)
            {
                return BadRequest("Could not add todolist.");
            }

            return RedirectToAction("Index");

        }

        // POST: Todo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            IdentityUser currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            

            if (!await _aspTodoService.DeleteListAsync(currentUser, id))
            {
                return BadRequest("Could not delete list!");
            }
            
            return RedirectToAction(nameof(Index));
        }        
        
    }
}
