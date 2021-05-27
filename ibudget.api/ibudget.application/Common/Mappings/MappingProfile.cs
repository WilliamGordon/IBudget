using AutoMapper;
using ibudget.application.Budgets.Queries;
using ibudget.application.Categories.Queries;
using ibudget.application.Transactions.Queries;
using ibudget.domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ibudget.application.Common.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Budget, BudgetDto>();
            CreateMap<BudgetDto, Budget>();
            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionDto, Transaction>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
