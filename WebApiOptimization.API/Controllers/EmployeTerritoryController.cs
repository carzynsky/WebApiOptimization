using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiOptimization.Application.Commands.EmployeeTerritory;
using WebApiOptimization.Application.Queries.EmployeeTerritoryQueries;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeTerritoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeTerritoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EmployeeTerritoryResponse>> GetAll([FromQuery] GetEmployeeTerritoriesQuery getEmployeeTerritoriesQuery)
        {
            var result = _mediator.Send(getEmployeeTerritoriesQuery);
            if (result == null)
                return NotFound($"EmployeeTerritory(ies) not found!");

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<EmployeeTerritoryResponse> Add(CreateEmployeeTerritoryCommand createEmployeeTerritoryCommand)
        {
            var result = _mediator.Send(createEmployeeTerritoryCommand);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<EmployeeTerritoryResponse> Update(int id, UpdateEmployeeTerritoryCommand updateEmployeeTerritoryCommand)
        {
            if (id != updateEmployeeTerritoryCommand.EmployeeId)
                return BadRequest($"EmployeeId does not match with updated data!");

            var result = _mediator.Send(updateEmployeeTerritoryCommand);
            if (result == null)
                return NotFound($"EmployeeTerritory with employee id={id} not found!");

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<EmployeeTerritoryResponse> Delete(int id)
        {
            var result = _mediator.Send(new DeleteEmployeeTerritoryCommand(id));
            if (result == null)
                return NotFound($"EmployeeTerritory with employee id={id} not found!");

            return Ok();
        }
    }
}
