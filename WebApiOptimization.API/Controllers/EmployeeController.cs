using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;

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
        public IReadOnlyList<Employee> Get()
        {
            throw new MissingMethodException();
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EmployeeResponse>> Add(CreateEmployeeCommand createEmployeeCommand)
        {
            var result = await _mediator.Send(createEmployeeCommand);
            return Ok(result);
        }
    }
}
