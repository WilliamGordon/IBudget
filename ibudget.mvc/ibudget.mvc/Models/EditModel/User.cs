using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibudget.mvc.Models.EditModel
{
    public class User
    {
        public string NickName { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public List<SelectListItem> Roles { get; set; } 
    }
}
