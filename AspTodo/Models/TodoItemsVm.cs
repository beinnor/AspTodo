using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspTodo.Models
{
    public class TodoItemsVm
    {
        public Guid TodoListId { get; set; }
        public List<TodoItem> TodoItems { get; set; }
    }
}
