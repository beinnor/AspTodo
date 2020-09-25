using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspTodo.Models
{
    public class TodoList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }                
        public string UserId { get; set; }
        public ICollection<TodoItem> TodoItems { get; set; }
    }
}
