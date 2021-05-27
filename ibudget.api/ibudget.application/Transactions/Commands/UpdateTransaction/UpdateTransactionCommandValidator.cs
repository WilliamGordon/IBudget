using FluentValidation;
using ibudget.application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ibudget.application.Transactions.Commands.UpdateTransaction
{
    public class UpdateTransactionCommandValidator : AbstractValidator<UpdateTransactionCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateTransactionCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Amount)
               .NotEmpty().WithMessage("Amount is required");

            RuleFor(v => v.Date)
               .NotEmpty().WithMessage("Date is required");
        }
    }
}
