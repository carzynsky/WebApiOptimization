using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
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
        private readonly IDistributedCache _distributedCache;
        public string EmployeeTerritoriesKey => "EmployeeTerritories";

        public EmployeTerritoryController(IMediator mediator, IDistributedCache distributedCache)
        {
            _mediator = mediator;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBuilder<IEnumerable<EmployeeTerritoryResponse>>>> GetAll([FromQuery] GetEmployeeTerritoriesQuery getEmployeeTerritoriesQuery)
        {
            if(getEmployeeTerritoriesQuery.PageNumber != 0 && getEmployeeTerritoriesQuery.PageSize != 0)
            {
                var result = await _mediator.Send(getEmployeeTerritoriesQuery);
                return Ok(result);
            }

            #region Distributed cache

            var objectFromCache = _distributedCache.Get(EmployeeTerritoriesKey);
            if (objectFromCache != null)
            {
                var json = Encoding.UTF8.GetString(objectFromCache);
                var result = JsonSerializer.Deserialize<ResponseBuilder<IEnumerable<EmployeeTerritoryResponse>>>(json);
                if (result != null)
                {
                    return Ok(result);
                }
            }

            var employeeTerritories = await _mediator.Send(getEmployeeTerritoriesQuery);
            var serialized = JsonSerializer.SerializeToUtf8Bytes(employeeTerritories);
            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

            _distributedCache.Set(EmployeeTerritoriesKey, serialized, cacheEntryOptions);
            return Ok(employeeTerritories);

            #endregion 

        }

        [HttpPost]
        public async Task<ActionResult<ResponseBuilder<EmployeeTerritoryResponse>>> Add(CreateEmployeeTerritoryCommand createEmployeeTerritoryCommand)
        {
            var result = await _mediator.Send(createEmployeeTerritoryCommand);
            if(result.Data == null)
            {
                return BadRequest(result);
            }

            return Created(string.Empty, result);
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseBuilder<EmployeeTerritoryResponse>>> Delete([FromQuery] DeleteEmployeeTerritoryCommand deleteEmployeeTerritoryCommand)
        {
            var result = await _mediator.Send(deleteEmployeeTerritoryCommand);
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
