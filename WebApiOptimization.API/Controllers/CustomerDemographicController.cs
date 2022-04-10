﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiOptimization.Application.Commands.CustomerDemographicCommands;
using WebApiOptimization.Application.Queries.CustomerDemographicQueries;
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
        public ActionResult<ResponseBuilder<IEnumerable<CustomerDemographicResponse>>> GetAll()
        {
            var result = _mediator.Send(new GetAllCustomerDemographicsQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ResponseBuilder<CustomerDemographicResponse>> GetById(int id)
        {
            var result = _mediator.Send(new GetCustomerDemographicByIdQuery(id));
            if (result == null)
                return NotFound($"CustomerDemographic with id={id} not found!");

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<ResponseBuilder<CustomerDemographicResponse>> Add(CreateCustomerDemographicCommand createCustomerDemographicCommand)
        {
            var result = _mediator.Send(createCustomerDemographicCommand);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ResponseBuilder<CustomerDemographicResponse>> Update(int id, UpdateCustomerDemographicCommand updateCustomerDemographicCommand)
        {
            int customerTypeId;
            if(!int.TryParse(updateCustomerDemographicCommand.CustomerTypeId, out customerTypeId))
            {
                return BadRequest($"Incorrect format of customer type id!");
            }

            if (id != customerTypeId)
                return BadRequest($"CustomerTypeId does not match with updated data!");

            var result = _mediator.Send(updateCustomerDemographicCommand);
            if (result == null)
                return NotFound($"CustomerDemographic with customer type id={id} not found!");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<ResponseBuilder<CustomerDemographicResponse>> Delete(string id)
        {
            var result = _mediator.Send(new DeleteCustomerDemographicCommand(id));
            if (result == null)
                return NotFound($"CustomerDemographic with customer type id={id} not found!");

            return Ok(result);
        }
    }
}
