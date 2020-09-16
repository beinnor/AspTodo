using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspTodo.Models
{
    public class AdminViewModel
    {
        public IdentityUser[] Administrators { get; set; }
        public IdentityUser[] Users { get; set; }
    }
}
