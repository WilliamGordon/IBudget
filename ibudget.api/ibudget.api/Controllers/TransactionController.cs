using ibudget.application.Transactions.Commands.CreateTransaction;
using ibudget.application.Transactions.Commands.DeleteTransaction;
using ibudget.application.Transactions.Commands.UpdateTransaction;
using ibudget.application.Transactions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibudget.api.Controllers
{
    [Authorize]
    public class TransactionController : ApiControllerBase
    {
#nullable enable
        [HttpGet("GetAllTransactionsForBudget/{budgetid}/{month}/{year}")]
        public async Task<ActionResult<List<TransactionDto>>> GetAllTransactionsForBudget(int budgetid, string? month, string? year)
        {
            return await Mediator.Send(new GetTransactionsQuery { BudgetId = budgetid, Month = month, Year = year });
        }
#nullable disable
        [HttpGet("GetTransaction/{id}")]
        public async Task<ActionResult<TransactionDto>> GetTransaction(int id)
        {
            return await Mediator.Send(new GetTransactionQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostAsync(CreateTransactionCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("UpdateTransaction/{id}")]
        public async Task<ActionResult> UpdateTransaction(int id, UpdateTransactionCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("UpdateTransactionCategory/{id}")]
        public async Task<ActionResult> UpdateTransactionCategory(int id, UpdateTransactionCategoryCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("DeleteTransaction/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTransactionCommand { Id = id });

            return NoContent();
        }
    }
}
