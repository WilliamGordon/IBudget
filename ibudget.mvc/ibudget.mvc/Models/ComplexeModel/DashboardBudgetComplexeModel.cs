using ibudget.mvc.Models.CreateModel;
using ibudget.mvc.Models.EditModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibudget.mvc.Models.ViewModel
{
    public class DashboardBudgetComplexeModel
    {
        public BudgetViewModel Budget { get; set; }
        public List<TransactionViewModel> Transactions { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public TransactionCreateModel Transaction { get; set; }
        public TransactionCategoryEditModel TransactionForEdit { get; set; }
        public CategoryCreateModel Category { get; set; }
    }
}
