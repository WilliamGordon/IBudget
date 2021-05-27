using ibudget.application.Categories.Commands.CreateCategory;
using ibudget.application.Categories.Commands.DeleteCategory;
using ibudget.application.Categories.Commands.UpdateCategory;
using ibudget.application.Categories.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ibudget.api.Controllers
{
    [Authorize]
    public class CategoryController : ApiControllerBase
    {
        [HttpGet("GetAllCategoriesForBudget/{budgetid}")]
        public async Task<ActionResult<List<CategoryDto>>> GetAllCategoriesForBudget(int budgetid)
        {
            return await Mediator.Send(new GetCategoriesQuery { BudgetId = budgetid });
        }

        [HttpGet("GetCategory/{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            return await Mediator.Send(new GetCategoryQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostAsync(CreateCategoryCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UpdateCategoryCommand command)
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
            await Mediator.Send(new DeleteCategoryCommand { Id = id });

            return NoContent();
        }
    }
}
