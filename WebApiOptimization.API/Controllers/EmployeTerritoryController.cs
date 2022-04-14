﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.EmployeeTerritoryCommands;
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
        public async Task<ActionResult<ResponseBuilder<IEnumerable<EmployeeTerritoryResponse>>>> GetAll([FromQuery] GetEmployeeTerritoriesQuery getEmployeeTerritoriesQuery)
        {
            var result = await _mediator.Send(getEmployeeTerritoriesQuery);
            if (result == null)
                return NotFound($"EmployeeTerritory(ies) not found!");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBuilder<EmployeeTerritoryResponse>>> Add(CreateEmployeeTerritoryCommand createEmployeeTerritoryCommand)
        {
            var result = await _mediator.Send(createEmployeeTerritoryCommand);
            return Ok(result);
        }

        /*
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
        */

        [HttpDelete]
        public async Task<ActionResult<ResponseBuilder<EmployeeTerritoryResponse>>> Delete([FromQuery] DeleteEmployeeTerritoryCommand deleteEmployeeTerritoryCommand)
        {
            var result = await _mediator.Send(deleteEmployeeTerritoryCommand);
            if (result == null)
                return NotFound($"EmployeeTerritory not found!");

            return Ok(result);
        }
    }
}
