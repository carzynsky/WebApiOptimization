using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiOptimization.Application.Commands.CustomerCustomerDemoCommands;
using WebApiOptimization.Application.Queries.CustomerCustomerDemoQueries;
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
        public ActionResult<ResponseBuilder<IEnumerable<CustomerCustomerDemoResponse>>> Get([FromQuery] GetCustomerCustomerDemoQuery getCustomerCustomerDemoQuery)
        {
            var result = _mediator.Send(getCustomerCustomerDemoQuery);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<ResponseBuilder<CustomerCustomerDemoResponse>> Add(CreateCustomerCustomerDemoCommand createCustomerCustomerDemoCommand)
        {
            var result = _mediator.Send(createCustomerCustomerDemoCommand);
            return Ok(result);
        }

        /* NO UPDATE FOR CustomerCustomerDemo
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

        */

        [HttpDelete]
        public ActionResult<ResponseBuilder<CustomerCustomerDemoResponse>> Delete([FromQuery] DeleteCustomerCustomerDemoCommand deleteCustomerCustomerDemoCommand)
        {
            if(deleteCustomerCustomerDemoCommand.CustomerId == null && deleteCustomerCustomerDemoCommand.CustomerTypeId == null)
            {
                return BadRequest("Parameters not provided!");
            }

            var result = _mediator.Send(deleteCustomerCustomerDemoCommand);
            if (result == null)
                return NotFound($"CustomerCustomerDemos not found!");

            return Ok(result);
        }
    }
}
