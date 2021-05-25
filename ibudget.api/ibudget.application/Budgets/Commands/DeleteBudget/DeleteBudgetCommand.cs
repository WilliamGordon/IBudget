using ibudget.application.Common.Exceptions;
using ibudget.application.Common.Interfaces;
using ibudget.domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ibudget.application.Budgets.Commands.DeleteBudget
{
    public class DeleteBudgetCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteBudgetCommandHandler : IRequestHandler<DeleteBudgetCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteBudgetCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Budgets
                .Where(b => b.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Budget), request.Id);
            }

            _context.Budgets.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
