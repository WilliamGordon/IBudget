using AutoMapper;
using ibudget.application.Common.Mappings;
using ibudget.domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ibudget.application.Budgets.Queries
{
    public class BudgetDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
