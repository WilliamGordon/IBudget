using FluentValidation;
using ibudget.application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ibudget.application.Transactions.Commands.UpdateTransaction
{
    public class UpdateTransactionCategoryCommandValidator : AbstractValidator<UpdateTransactionCategoryCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateTransactionCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.CategoryId)
               .NotEmpty().WithMessage("CategoryId is required");
        }
    }
}
