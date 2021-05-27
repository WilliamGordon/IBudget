using FluentValidation;
using ibudget.application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ibudget.application.Budgets.Commands.CreateBudget
{
    public class CreateBudgetCommandValidator : AbstractValidator<CreateBudgetCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateBudgetCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.UserId)
               .NotEmpty().WithMessage("UserId is required");

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");
        }
    }
}
