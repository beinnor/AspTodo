using System;
using System.ComponentModel.DataAnnotations;

namespace AspTodo.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Descripton { get; set; }
        [Display(Name = "Completed  ")]
        public bool IsComplete { get; set; }
        public string UserId { get; set; }
    }
}
