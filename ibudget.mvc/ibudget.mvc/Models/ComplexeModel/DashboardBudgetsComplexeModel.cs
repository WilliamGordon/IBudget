using ibudget.mvc.Models.CreateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibudget.mvc.Models.ViewModel
{
    public class DashboardBudgetsComplexeModel
    {
        public List<BudgetViewModel> Budgets { get; set; }
        public BudgetCreateModel Budget { get; set; }
    }
}
