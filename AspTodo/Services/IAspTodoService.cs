using AspTodo.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspTodo.Services
{
    public interface IAspTodoService
    {
        Task<TodoList[]> GetTodoListsAsync(IdentityUser user);
        Task<bool> AddListAsync(IdentityUser user, TodoList newTodoList);
        Task<bool> DeleteListAsync(IdentityUser user, Guid todoListId);

        Task<TodoItem[]> GetTodoItemsAsync(IdentityUser user, Guid todoListId);
        Task<bool> AddTodoItemAsync(IdentityUser user, Guid todoListId, TodoItem newTodoItem);
        Task<bool> DeleteTodoItemAsync(IdentityUser user, Guid todoItemId);

    }
}
