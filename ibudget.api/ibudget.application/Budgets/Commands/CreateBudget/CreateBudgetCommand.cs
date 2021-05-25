using ibudget.application.Common.Interfaces;
using ibudget.domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ibudget.application.Budgets.Commands.CreateBudget
{
    public class CreateBudgetCommand: IRequest<int>
    {
        public string UserId { get; set; }
        public string Name { get; set; }
    }

    public class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateBudgetCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
        {
            var entity = new Budget();
            entity.UserId = request.UserId;
            entity.Name = request.Name;
            _context.Budgets.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
