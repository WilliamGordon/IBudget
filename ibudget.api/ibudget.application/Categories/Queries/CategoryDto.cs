using ibudget.domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ibudget.application.Categories.Queries
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public int BudgetId { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
