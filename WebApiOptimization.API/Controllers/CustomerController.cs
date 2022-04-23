using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.CustomerCommands;
using WebApiOptimization.Application.Queries.CustomerQueries;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBuilder<IEnumerable<CustomerResponse>>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllCustomersQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseBuilder<CustomerResponse>>> GetById(string id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery(id));
            if (result.Data == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBuilder<CustomerResponse>>> Add(CreateCustomerCommand createCustomerCommand)
        {
            var result = await _mediator.Send(createCustomerCommand);
            if(result.Data == null)
            {
                return BadRequest(result);
            }

            return Created(string.Empty, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseBuilder<CustomerResponse>>> Update(string id, UpdateCustomerCommand updateCustomerCommand)
        {
            if (id != updateCustomerCommand.CustomerID)
            {
                return BadRequest($"CustomerId does not match with updated data!");
            }

            var result = await _mediator.Send(updateCustomerCommand);
            if (result.Data == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseBuilder<CustomerResponse>>> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand(id));
            if (result.Data == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
