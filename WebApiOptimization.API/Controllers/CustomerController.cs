using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiOptimization.Application.Commands.Customer;
using WebApiOptimization.Application.Queries.Customer;
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
        public ActionResult<IEnumerable<CustomerResponse>> GetAll()
        {
            var result = _mediator.Send(new GetAllCustomersQuery()).Result;
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<CustomerResponse> GetById(int id)
        {
            var result = _mediator.Send(new GetCustomerByIdQuery(id)).Result;
            if (result == null)
                return NotFound($"Category with id={id} not found!");

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<CustomerResponse> Add(CreateCustomerCommand createCustomerCommand)
        {
            var result = _mediator.Send(createCustomerCommand);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CustomerResponse> Update(int id, UpdateCustomerCommand updateCustomerCommand)
        {
            if (id != updateCustomerCommand.CustomerId)
                return BadRequest($"CustomerId does not match with updated data!");

            var result = _mediator.Send(updateCustomerCommand).Result;
            if (result == null)
                return NotFound($"Customer with id={id} not found!");

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CustomerResponse> Delete(int id)
        {
            var result = _mediator.Send(new DeleteCustomerCommand(id)).Result;
            if (result == null)
                return NotFound($"Customer with id={id} not found!");

            return Ok();
        }
    }
}
