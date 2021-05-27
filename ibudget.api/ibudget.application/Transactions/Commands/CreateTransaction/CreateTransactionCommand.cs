using ibudget.application.Common.Interfaces;
using ibudget.domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ibudget.application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommand : IRequest<int>
    {
        public int BudgetId { get; set; }
        public int? CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Transmitter { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
    }

    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = new Transaction
            {
                Budget = await _context.Budgets.SingleOrDefaultAsync(x => x.Id == request.BudgetId), 
                Category = await _context.Categories.SingleOrDefaultAsync(x => x.Id == request.CategoryId),
                Amount = request.Amount,
                Description = request.Description,
                Date = request.Date,
                Transmitter = request.Transmitter,
                Receiver = request.Receiver,
                Message = request.Message,
            };

            _context.Transactions.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
