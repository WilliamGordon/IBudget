using AutoMapper;
using ibudget.application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ibudget.application.Budgets.Queries
{
    public class GetBudgetsQuery : IRequest<List<BudgetDto>>
    {
        public string UserId { get; set; }
    }

    public class GetBudgetsQueryHandler : IRequestHandler<GetBudgetsQuery, List<BudgetDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBudgetsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<BudgetDto>> Handle(GetBudgetsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BudgetDto>>(await _context.Budgets.Where(x => x.UserId == request.UserId).ToListAsync());
        }
    }
}
