using System;
using System.ComponentModel.DataAnnotations;

namespace AspTodo.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }                
        public string Descripton { get; set; }
        public bool IsComplete { get; set; }

        public Guid TodoListId { get; set; }
        public TodoList TodoList { get; set; }
    }
}
