using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.ShipperCommands;
using WebApiOptimization.Application.Queries.ShipperQueries;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDistributedCache _distributedCache;
        public string ShippersKey => "Shippers";

        public ShipperController(IMediator mediator, IDistributedCache distributedCache)
        {
            _mediator = mediator;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBuilder<IEnumerable<ShipperResponse>>>> GetAll([FromQuery] GetAllShippersQuery getAllShippersQuery)
        {
            if(getAllShippersQuery.PageNumber != 0 && getAllShippersQuery.PageSize != 0)
            {
                var result = await _mediator.Send(getAllShippersQuery);
                return Ok(result);
            }

            #region Distributed cache

            var objectFromCache = _distributedCache.Get(ShippersKey);
            if (objectFromCache != null)
            {
                var json = Encoding.UTF8.GetString(objectFromCache);
                var result = JsonSerializer.Deserialize<ResponseBuilder<IEnumerable<ShipperResponse>>>(json);
                if (result != null)
                {
                    return Ok(result);
                }
            }

            var shippers = await _mediator.Send(getAllShippersQuery);
            var serialized = JsonSerializer.SerializeToUtf8Bytes(shippers);
            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

            _distributedCache.Set(ShippersKey, serialized, cacheEntryOptions);
            return Ok(shippers);

            #endregion
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<ShipperResponse>>> GetById(int id)
        {
            var result = await _mediator.Send(new GetShipperByIdQuery(id));
            if (result.Data == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBuilder<ShipperResponse>>> Add(CreateShipperCommand createShipperCommand)
        {
            var result = await _mediator.Send(createShipperCommand);
            if(result.Data == null)
            {
                return BadRequest(result);
            }

            return Created(string.Empty, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<ShipperResponse>>> Update(int id, UpdateShipperCommand updateShipperCommand)
        {
            if (id != updateShipperCommand.ShipperId)
            {
                return BadRequest($"ShipperId does not match with updated data!");
            }

            var result = await _mediator.Send(updateShipperCommand);
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<ShipperResponse>>> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteShipperCommand(id));
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
