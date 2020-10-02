using AspTodo.Data;
using AspTodo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            var results = await _context.SaveChangesAsync();

            return results == 1;
            
        }

        public Task<bool> AddTodoItemAsync(IdentityUser user, Guid todoListId, TodoItem newTodoItem)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteListAsync(IdentityUser user, Guid todoListId)
        {
            var todoList = await _context.TodoList.FindAsync(todoListId);

            if (todoList.UserId != user.Id)
            {
                return false;
            }
            _context.TodoList.Remove(todoList);
            var results = await _context.SaveChangesAsync();

            return results == 1;
        }

        public Task<bool> DeleteTodoItemAsync(IdentityUser user, Guid todoItemId)
        {
            throw new NotImplementedException();
        }

        public async Task<TodoItem[]> GetTodoItemsAsync(IdentityUser user, Guid todoListId)
        {
            // TODO: gjør noe a la dette

            var items = await _context.TodoItem
                .Where(x => x.TodoList.UserId == user.Id && x.TodoListId == todoListId)
                .ToArrayAsync();

            return items;
        }

        public async Task<TodoList[]> GetTodoListsAsync(IdentityUser user)
        {            
            // Get todo-lists from database

            var lists = await _context.TodoList
                .Where(x => x.UserId == user.Id)
                .ToArrayAsync();

            // Pass the view to model and render            

            return lists;
        }
    }
}
