using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public ActionResult<ResponseBuilder<IEnumerable<CustomerResponse>>> GetAll()
        {
            var result = _mediator.Send(new GetAllCustomersQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseBuilder<CustomerResponse>> GetById(string id)
        {
            var result = _mediator.Send(new GetCustomerByIdQuery(id));
            if (result == null)
                return NotFound($"Customer with id={id} not found!");

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<ResponseBuilder<CustomerResponse>> Add(CreateCustomerCommand createCustomerCommand)
        {
            var result = _mediator.Send(createCustomerCommand);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public ActionResult<ResponseBuilder<CustomerResponse>> Update(string id, UpdateCustomerCommand updateCustomerCommand)
        {
            if (id != updateCustomerCommand.CustomerID)
                return BadRequest($"CustomerId does not match with updated data!");

            var result = _mediator.Send(updateCustomerCommand);
            if (result == null)
                return NotFound($"Customer with id={id} not found!");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<ResponseBuilder<CustomerResponse>> Delete(string id)
        {
            var result = _mediator.Send(new DeleteCustomerCommand(id));
            if (result == null)
                return NotFound($"Customer with id={id} not found!");

            return Ok(result);
        }
    }
}
