using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.EmployeeCommands;
using WebApiOptimization.Application.Queries.EmployeeQueries;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDistributedCache _distributedCache;
        public string EmployeesKey => "Employees";

        public EmployeeController(IMediator mediator, IDistributedCache distributedCache)
        {
            _mediator = mediator;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBuilder<IEnumerable<EmployeeResponse>>>> GetAll([FromQuery] GetAllEmployeesQuery getAllEmployeesQuery)
        {
            if(getAllEmployeesQuery.PageNumber != 0 && getAllEmployeesQuery.PageSize != 0)
            {
                var result = await _mediator.Send(getAllEmployeesQuery);
                return Ok(result);
            }

            #region Distributed cache

            var objectFromCache = _distributedCache.Get(EmployeesKey);
            if (objectFromCache != null)
            {
                var json = Encoding.UTF8.GetString(objectFromCache);
                var result = JsonSerializer.Deserialize<ResponseBuilder<IEnumerable<EmployeeResponse>>>(json);
                if (result != null)
                {
                    return Ok(result);
                }
            }

            var employees = await _mediator.Send(new GetAllEmployeesQuery());
            var serialized = JsonSerializer.SerializeToUtf8Bytes(employees);
            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

            _distributedCache.Set(EmployeesKey, serialized, cacheEntryOptions);
            return Ok(employees);

            #endregion
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<EmployeeResponse>>> GetById(int id)
        {
            var result = await _mediator.Send(new GetEmployeeByIdQuery(id));
            if (result.Data == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<ResponseBuilder<EmployeeResponse>>> Add(CreateEmployeeCommand createEmployeeCommand)
        {
            var result = await _mediator.Send(createEmployeeCommand);
            if(result.Data == null)
            {
                return BadRequest(result);
            }

            return Created(string.Empty, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<EmployeeResponse>>> Update(int id, UpdateEmployeeCommand updateEmployeeCommand)
        {
            if (id != updateEmployeeCommand.EmployeeId)
            {
                return BadRequest($"EmployeeId does not match with updated data!");
            }

            var result = await _mediator.Send(updateEmployeeCommand);
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<EmployeeResponse>>> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand(id));
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
