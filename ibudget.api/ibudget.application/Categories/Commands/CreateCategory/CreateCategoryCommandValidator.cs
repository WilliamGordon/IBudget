using FluentValidation;
using ibudget.application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ibudget.application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.BudgetId)
               .NotEmpty().WithMessage("BudgetId is required");
            RuleFor(v => v.Name)
               .NotEmpty().WithMessage("Name is required");
        }
    }
}
