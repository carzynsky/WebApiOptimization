using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.TerritoryCommands;
using WebApiOptimization.Application.Queries.TerritoryQueries;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerritoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDistributedCache _distributedCache;
        public string TerritoriesKey => "Territories";

        public TerritoryController(IMediator mediator, IDistributedCache distributedCache)
        {
            _mediator = mediator;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBuilder<IEnumerable<TerritoryResponse>>>> GetAll([FromQuery] GetAllTerritoriesQuery getAllTerritoriesQuery)
        {
            if(getAllTerritoriesQuery.PageNumber != 0 && getAllTerritoriesQuery.PageSize != 0)
            {
                var result = await _mediator.Send(getAllTerritoriesQuery);
                return Ok(result);
            }

            #region Distributed cache

            var objectFromCache = _distributedCache.Get(TerritoriesKey);
            if (objectFromCache != null)
            {
                var json = Encoding.UTF8.GetString(objectFromCache);
                var result = JsonSerializer.Deserialize<ResponseBuilder<IEnumerable<TerritoryResponse>>>(json);
                if (result != null)
                {
                    return Ok(result);
                }
            }

            var territories = await _mediator.Send(getAllTerritoriesQuery);
            var serialized = JsonSerializer.SerializeToUtf8Bytes(territories);
            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

            _distributedCache.Set(TerritoriesKey, serialized, cacheEntryOptions);
            return Ok(territories);

            #endregion

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseBuilder<TerritoryResponse>>> GetById(string id)
        {
            var result = await _mediator.Send(new GetTerritoryByIdQuery(id));
            if (result.Data == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBuilder<TerritoryResponse>>> Add(CreateTerritoryCommand createTerritoryCommand)
        {
            var result = await _mediator.Send(createTerritoryCommand);
            if(result.Data == null)
            {
                return BadRequest(result);
            }

            return Created(string.Empty, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseBuilder<TerritoryResponse>>> Update(string id, UpdateTerritoryCommand updateTerritoryCommand)
        {
            if (id != updateTerritoryCommand.TerritoryId)
            {
                return BadRequest($"TerritoryId does not match with updated data!");
            }

            var result = await _mediator.Send(updateTerritoryCommand);
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseBuilder<TerritoryResponse>>> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteTerritoryCommand(id));
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
