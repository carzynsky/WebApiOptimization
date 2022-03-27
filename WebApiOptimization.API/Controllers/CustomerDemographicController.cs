using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.CustomerDemographic;
using WebApiOptimization.Application.Queries.CustomerDemographic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDemographicController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerDemographicController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerDemographicResponse>> GetAll()
        {
            var result = _mediator.Send(new GetAllCustomerDemographicsQuery()).Result;
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<CustomerDemographicResponse> GetById(int id)
        {
            var result = _mediator.Send(new GetCustomerDemographicByIdQuery(id)).Result;
            if (result == null)
                return NotFound($"CustomerDemographic with id={id} not found!");

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<CustomerDemographicResponse> Add(CreateCustomerDemographicCommand createCustomerDemographicCommand)
        {
            var result = _mediator.Send(createCustomerDemographicCommand);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CustomerDemographicResponse> Update(int id, UpdateCustomerDemographicCommand updateCustomerDemographicCommand)
        {
            int customerTypeId;
            if(!int.TryParse(updateCustomerDemographicCommand.CustomerTypeId, out customerTypeId))
            {
                return BadRequest($"Incorrect format of customer type id!");
            }

            if (id != customerTypeId)
                return BadRequest($"CustomerTypeId does not match with updated data!");

            var result = _mediator.Send(updateCustomerDemographicCommand).Result;
            if (result == null)
                return NotFound($"CustomerDemographic with customer type id={id} not found!");

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CustomerDemographicResponse> Delete(int id)
        {
            var result = _mediator.Send(new DeleteCustomerDemographicCommand(id)).Result;
            if (result == null)
                return NotFound($"CustomerDemographic with customer type id={id} not found!");

            return Ok();
        }
    }
}
