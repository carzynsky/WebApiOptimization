using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiOptimization.Application.Commands.EmployeeCommands;
using WebApiOptimization.Application.Queries.EmployeeQueries;
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
        public ActionResult<ResponseBuilder<IEnumerable<EmployeeResponse>>> GetAll()
        {
            var result = _mediator.Send(new GetAllEmployeesQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ResponseBuilder<EmployeeResponse>> GetById(int id)
        {
            var result = _mediator.Send(new GetEmployeeByIdQuery(id));
            if (result == null)
                return NotFound($"Employee with id={id} not found!");

            return Ok(result);
        }
        
        [HttpPost]
        public ActionResult<ResponseBuilder<EmployeeResponse>> Add(CreateEmployeeCommand createEmployeeCommand)
        {
            var result = _mediator.Send(createEmployeeCommand);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ResponseBuilder<EmployeeResponse>> Update(int id, UpdateEmployeeCommand updateEmployeeCommand)
        {
            if (id != updateEmployeeCommand.EmployeeId)
                return BadRequest($"EmployeeId does not match with updated data!");

            var result = _mediator.Send(updateEmployeeCommand);
            if (result == null)
                return NotFound($"Employee with id={id} not found!");

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ResponseBuilder<EmployeeResponse>> Delete(int id)
        {
            var result = _mediator.Send(new DeleteEmployeeCommand(id));
            if (result == null)
                return NotFound($"Employee with id={id} not found!");

            return Ok();
        }
    }
}
