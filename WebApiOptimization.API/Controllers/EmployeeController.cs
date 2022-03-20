using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiOptimization.Application.Commands;
using WebApiOptimization.Application.Queries;
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
        public ActionResult<IReadOnlyList<EmployeeResponse>> GetAll()
        {
            var result = _mediator.Send(new GetAllEmployeesQuery());
            return Ok(result);
        }
        
        [HttpPost]
        public ActionResult<EmployeeResponse> Add(CreateEmployeeCommand createEmployeeCommand)
        {
            var result = _mediator.Send(createEmployeeCommand);
            return Ok(result);
        }
    }
}
