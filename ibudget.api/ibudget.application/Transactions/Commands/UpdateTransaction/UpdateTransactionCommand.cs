using ibudget.application.Common.Exceptions;
using ibudget.application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace ibudget.application.Transactions.Commands.UpdateTransaction
{
    public class UpdateTransactionCommand : IRequest
    {
        public int Id { get; set; }
        public int CategoryId{ get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Transmitter { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
    }

    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Transactions.FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Transaction), request.Id);
            }
            entity.Amount = request.Amount;
            entity.Description = request.Description;
            entity.Date = request.Date;
            entity.Transmitter = request.Transmitter;
            entity.Receiver = request.Receiver;
            entity.Message = request.Message;
            entity.Category = await _context.Categories.FindAsync(request.CategoryId);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
