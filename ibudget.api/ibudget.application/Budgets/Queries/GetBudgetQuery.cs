using AutoMapper;
using ibudget.application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ibudget.application.Budgets.Queries
{
    public class GetBudgetQuery: IRequest<BudgetDto>
    {
        public int Id { get; set; }
    }

    public class GetBudgetQueryHandler : IRequestHandler<GetBudgetQuery, BudgetDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBudgetQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BudgetDto> Handle(GetBudgetQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<BudgetDto>(await _context.Budgets.SingleOrDefaultAsync(x => x.Id == request.Id));
        }
    }
}
