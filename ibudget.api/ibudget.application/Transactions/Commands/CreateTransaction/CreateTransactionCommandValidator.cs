using ibudget.application.Common.Interfaces;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace ibudget.application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateTransactionCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.BudgetId)
               .NotEmpty().WithMessage("BudgetId is required");

            RuleFor(v => v.Amount)
               .NotEmpty().WithMessage("Amount is required");

            RuleFor(v => v.Date)
               .Must(BeAValidDate).WithMessage("Date is required");

            RuleFor(v => v.Description)
               .NotEmpty().WithMessage("Description is required");

            RuleFor(v => v.Message)
               .NotEmpty().WithMessage("Message is required");

            RuleFor(v => v.Receiver)
               .NotEmpty().WithMessage("Receiver is required");

            RuleFor(v => v.Transmitter)
               .NotEmpty().WithMessage("Transmitter is required");
        }
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
