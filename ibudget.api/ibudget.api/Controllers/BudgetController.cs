using ibudget.application.Budgets.Commands.CreateBudget;
using ibudget.application.Budgets.Commands.DeleteBudget;
using ibudget.application.Budgets.Commands.UpdateBudget;
using ibudget.application.Budgets.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ibudget.api.Controllers
{
    [Authorize]
    public class BudgetController : ApiControllerBase
    {
        [HttpGet("GetAllBudgetsForUser/{userid}")]
        public async Task<ActionResult<List<BudgetDto>>> Get(string userid)
        {
            return await Mediator.Send(new GetBudgetsQuery { UserId = userid });
        }

        [HttpGet("GetBudget/{id}")]
        public async Task<ActionResult<BudgetDto>> Get(int id)
        {
            return await Mediator.Send(new GetBudgetQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostAsync(CreateBudgetCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UpdateBudgetCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteBudgetCommand { Id = id });

            return NoContent();
        }
    }
}
