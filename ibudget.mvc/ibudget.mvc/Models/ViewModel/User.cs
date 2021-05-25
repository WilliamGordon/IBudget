using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibudget.mvc.Models.ViewModel
{
    public class User
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public string FullName { get; set; }
        public string Picture { get; set; }
        public string Role { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastLogin { get; set; }
        public string LoginsCount { get; set; }
    }
}
