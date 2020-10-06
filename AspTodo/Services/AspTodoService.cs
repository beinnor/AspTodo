using AspTodo.Data;
using AspTodo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;

namespace AspTodo.Services
{
    public class AspTodoService : IAspTodoService
    {
        private readonly ApplicationDbContext _context;

        public AspTodoService (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddListAsync(IdentityUser user, TodoList newTodoList)
        {

            newTodoList.Id = Guid.NewGuid();
            newTodoList.UserId = user.Id;
            _context.Add(newTodoList);
            int results = await _context.SaveChangesAsync();

            return results == 1;
            
        }

        public async Task<bool> AddTodoItemAsync(IdentityUser user, TodoItem newTodoItem)
        {          

            TodoList tempTodoList = await _context.TodoList.FindAsync(newTodoItem.TodoListId);

            newTodoItem.Id = Guid.NewGuid();
            newTodoItem.TodoList = tempTodoList;
            _context.Add(newTodoItem);
            int results = await _context.SaveChangesAsync();

            return results == 1;
        }

        public async Task<bool> DeleteListAsync(IdentityUser user, Guid todoListId)
        {
            TodoList todoList = await _context.TodoList.FindAsync(todoListId);

            if (todoList.UserId != user.Id)
            {
                return false;
            }
            _context.TodoList.Remove(todoList);
            int results = await _context.SaveChangesAsync();

            return results == 1;
        }

        public async Task<bool> DeleteTodoItemAsync(IdentityUser user, Guid todoItemId)
        {
            TodoItem todoItem = await _context.TodoItem.FindAsync(todoItemId);

            TodoList todoList = await _context.TodoList.FindAsync(todoItem.TodoListId);

            if (todoList.UserId != user.Id)
            {
                return false;
            }
            _context.TodoItem.Remove(todoItem);
            int results = await _context.SaveChangesAsync();

            return results == 1;
        }

        public async Task<List<TodoItem>> GetTodoItemsAsync(IdentityUser user, Guid todoListId)
        {            

            List<TodoList> list = await _context.TodoList
                .Where(x => x.UserId == user.Id && x.Id == todoListId)
                .Include(x => x.TodoItems)
                .ToListAsync();

            List<TodoItem> items = list[0].TodoItems.ToList<TodoItem>();           

            return items;
        }

        public async Task<List<TodoList>> GetTodoListsAsync(IdentityUser user)
        {            
            // Get todo-lists from database

            List<TodoList> lists = await _context.TodoList
                .Where(x => x.UserId == user.Id)
                .ToListAsync<TodoList>();

            // Pass the view to model and render            

            return lists;
        }
    }
}
