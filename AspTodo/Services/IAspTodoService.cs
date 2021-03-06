﻿using AspTodo.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspTodo.Services
{
    public interface IAspTodoService
    {
        Task<List<TodoList>> GetTodoListsAsync(IdentityUser user);
        Task<bool> AddListAsync(IdentityUser user, TodoList newTodoList);
        Task<bool> DeleteListAsync(IdentityUser user, Guid todoListId);

        Task<List<TodoItem>> GetTodoItemsAsync(IdentityUser user, Guid todoListId);
        Task<bool> AddTodoItemAsync(IdentityUser user, TodoItem newTodoItem);
        Task<bool> DeleteTodoItemAsync(IdentityUser user, Guid todoItemId);

        Task<bool> MarkItemDoneAsync(Guid ItemId, Guid ListId);

    }
}
