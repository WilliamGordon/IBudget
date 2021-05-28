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

namespace ibudget.application.Transactions.Queries
{
    public class GetTransactionsQuery : IRequest<List<TransactionDto>>
    {
        public int BudgetId { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }

    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<TransactionDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTransactionsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            if(String.IsNullOrEmpty(request.Month) || String.IsNullOrEmpty(request.Year))
            {
                request.Month = DateTime.Now.Month.ToString();
                request.Year = DateTime.Now.Year.ToString();
            }

            return _mapper.Map<List<TransactionDto>>(await _context.Transactions.Where(x => 
                x.Budget.Id == request.BudgetId &&
                x.Date.Month.ToString() == request.Month &&
                x.Date.Year.ToString() == request.Year).ToListAsync());
        }
    }
}
