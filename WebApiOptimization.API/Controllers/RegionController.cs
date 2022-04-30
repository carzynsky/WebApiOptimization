using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.RegionCommands;
using WebApiOptimization.Application.Queries.RegionQueries;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDistributedCache _distributedCache;
        public string RegionsKey => "Regions";

        public RegionController(IMediator mediator, IDistributedCache distributedCache)
        {
            _mediator = mediator;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBuilder<IEnumerable<RegionResponse>>>> GetAll([FromQuery] GetAllRegionsQuery getAllRegionsQuery)
        {
            if(getAllRegionsQuery.PageNumber != 0 && getAllRegionsQuery.PageSize != 0)
            {
                var result = await _mediator.Send(getAllRegionsQuery);
                return Ok(result);
            }

            #region Distributed cache

            var objectFromCache = _distributedCache.Get(RegionsKey);
            if (objectFromCache != null)
            {
                var json = Encoding.UTF8.GetString(objectFromCache);
                var result = JsonSerializer.Deserialize<ResponseBuilder<IEnumerable<RegionResponse>>>(json);
                if (result != null)
                {
                    return Ok(result);
                }
            }

            var regions = await _mediator.Send(getAllRegionsQuery);
            var serialized = JsonSerializer.SerializeToUtf8Bytes(regions);
            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

            _distributedCache.Set(RegionsKey, serialized, cacheEntryOptions);
            return Ok(regions);

            #endregion 

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<RegionResponse>>> GetById(int id)
        {
            var result = await _mediator.Send(new GetRegionByIdQuery(id));
            if (result.Data == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBuilder<RegionResponse>>> Add(CreateRegionCommand createRegionCommand)
        {
            var result = await _mediator.Send(createRegionCommand);
            if(result.Data == null)
            {
                return BadRequest(result);
            }

            return Created(string.Empty, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<RegionResponse>>> Update(int id, UpdateRegionCommand updateRegionCommand)
        {
            if (id != updateRegionCommand.RegionId)
            {
                return BadRequest($"RegionId does not match with updated data!");
            }

            var result = await _mediator.Send(updateRegionCommand);
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<RegionResponse>>> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteRegionCommand(id));
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
