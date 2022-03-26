using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiOptimization.Application.Commands.Employee;
using WebApiOptimization.Application.Queries.Employee;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EmployeeResponse>> GetAll()
        {
            var result = _mediator.Send(new GetAllEmployeesQuery()).Result;
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<EmployeeResponse> GetById(int id)
        {
            var result = _mediator.Send(new GetEmployeeByIdQuery(id)).Result;
            if (result == null)
                return NotFound($"Employee with id={id} not found!");

            return Ok(result);
        }
        
        [HttpPost]
        public ActionResult<EmployeeResponse> Add(CreateEmployeeCommand createEmployeeCommand)
        {
            var result = _mediator.Send(createEmployeeCommand);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<EmployeeResponse> Update(int id, UpdateEmployeeCommand updateEmployeeCommand)
        {
            if (id != updateEmployeeCommand.EmployeeId)
                return BadRequest($"EmployeeId does not match with updated data!");

            var result = _mediator.Send(updateEmployeeCommand).Result;
            if (result == null)
                return NotFound($"Employee with id={id} not found!");

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<EmployeeResponse> Delete(int id)
        {
            var result = _mediator.Send(new DeleteEmployeeCommand(id)).Result;
            if (result == null)
                return NotFound($"Employee with id={id} not found!");

            return Ok();
        }
    }
}
