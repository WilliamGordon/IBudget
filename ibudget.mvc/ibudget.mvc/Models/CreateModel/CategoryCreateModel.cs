using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibudget.mvc.Models.CreateModel
{
    public class CategoryCreateModel
    {
        public int BudgetId { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
