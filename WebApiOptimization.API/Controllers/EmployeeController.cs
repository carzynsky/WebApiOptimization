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
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;
        public string EmployeesKey => "Employees";

        public EmployeeController(IMediator mediator, IMemoryCache memoryCache, IDistributedCache ditributedCache)
        {
            _mediator = mediator;
            _memoryCache = memoryCache;
            _distributedCache = ditributedCache;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBuilder<IEnumerable<EmployeeResponse>>>> GetAll()
        {
            /*
            var result = await _mediator.Send(new GetAllEmployeesQuery());
            return Ok(result);
            */

            /*
            // InMemory Cache
            ResponseBuilder<IEnumerable<EmployeeResponse>> response;
            if (!_memoryCache.TryGetValue(EmployeesKey, out response))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

                response = await _mediator.Send(new GetAllEmployeesQuery());
                _memoryCache.Set(EmployeesKey, response, cacheEntryOptions);
            }

            return Ok(response);
            */

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
