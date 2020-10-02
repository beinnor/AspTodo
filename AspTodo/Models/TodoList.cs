using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace AspTodo.Models
{
    public class TodoList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public ICollection<TodoItem> TodoItems { get; set; }
    }
}