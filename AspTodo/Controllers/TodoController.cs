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
    [Authorize]
    public class TodoController : Controller
    {        
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TodoController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {            
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(Guid Id)
        {
            var list = _context.TodoList
                .Single(x => x.Id == Id);

            _context.Entry(list)
                .Collection(x => x.TodoItems)
                .Load();
            

            // return View(list);
            return View(list.TodoItems);
        }

        
    }
}
