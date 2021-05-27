using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibudget.mvc.Models.EditModel
{
    public class TransactionCategoryEditModel
    {
        public int Id { get; set; }
        public int BudgetId { get; set; }
        public int CategoryId { get; set; }
        public List<SelectListItem> Categories { get; } = new List<SelectListItem>();
    }
}
