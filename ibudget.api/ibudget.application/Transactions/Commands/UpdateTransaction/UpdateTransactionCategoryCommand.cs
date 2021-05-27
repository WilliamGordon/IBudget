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
    public class UpdateTransactionCategoryCommand : IRequest
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
    }

    public class UpdateTransactionCategoryCommandHandler : IRequestHandler<UpdateTransactionCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTransactionCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTransactionCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Transactions.FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Transaction), request.Id);
            }
            entity.CategoryId = request.CategoryId;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
