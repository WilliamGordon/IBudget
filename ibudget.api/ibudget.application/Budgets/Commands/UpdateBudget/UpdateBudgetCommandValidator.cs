using FluentValidation;
using ibudget.application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ibudget.application.Budgets.Commands.UpdateBudget
{
    public class UpdateBudgetCommandValidator : AbstractValidator<UpdateBudgetCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateBudgetCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
        }

        public async Task<bool> BeUniqueTitle(UpdateBudgetCommand model, string name, CancellationToken cancellationToken)
        {
            return await _context.Budgets
                .Where(l => l.Id != model.Id)
                .AllAsync(l => l.Name != name);
        }
    }
}
