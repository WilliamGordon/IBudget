using ibudget.application.Common.Interfaces;
using ibudget.domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ibudget.application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<int>
    {
        public int BudgetId { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
    }
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new Category();
            entity.BudgetId = request.BudgetId;
            if(request.ParentId == 0)
            {
                entity.ParentId = null;
            } 
            else
            {
                entity.ParentId = request.ParentId;
            }
            entity.Name = request.Name;
            _context.Categories.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
