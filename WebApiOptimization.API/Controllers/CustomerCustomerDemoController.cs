using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiOptimization.Application.Commands.CustomerCustomerDemo;
using WebApiOptimization.Application.Queries.CustomerCustomerDemo;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerCustomerDemoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerCustomerDemoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerCustomerDemoResponse>> GetAll()
        {
            var result = _mediator.Send(new GetAllCustomerCustomerDemosQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<CustomerCustomerDemoResponse> GetById(int id)
        {
            var result = _mediator.Send(new GetCustomerCustomerDemoByIdQuery(id));
            if (result == null)
                return NotFound($"CustomerCustomerDemo with id={id} not found!");

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<CustomerCustomerDemoResponse> Add(CreateCustomerCustomerDemoCommand createCustomerCustomerDemoCommand)
        {
            var result = _mediator.Send(createCustomerCustomerDemoCommand);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CustomerCustomerDemoResponse> Update(int id, UpdateCustomerCustomerDemoCommand updateCustomerCustomerDemoCommand)
        {
            if (id != updateCustomerCustomerDemoCommand.CustomerId)
                return BadRequest($"CustomerId does not match with updated data!");

            var result = _mediator.Send(updateCustomerCustomerDemoCommand);
            if (result == null)
                return NotFound($"CustomerCustomerDemo with customer id={id} not found!");

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CustomerCustomerDemoResponse> Delete(int id)
        {
            var result = _mediator.Send(new DeleteCustomerCustomerDemoCommand(id));
            if (result == null)
                return NotFound($"CustomerCustomerDemo with customer id={id} not found!");

            return Ok();
        }
    }
}
