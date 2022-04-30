using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.OrderCommands;
using WebApiOptimization.Application.Queries.OrderQueries;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDistributedCache _distributedCache;
        public string Orders => "Orders";

        public OrderController(IMediator mediator, IDistributedCache distributedCache)
        {
            _mediator = mediator;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBuilder<IEnumerable<OrderResponse>>>> GetAll([FromQuery] GetAllOrdersQuery getAllOrdersQuery)
        {
            if(getAllOrdersQuery.PageNumber != 0 && getAllOrdersQuery.PageSize != 0)
            {
                var response = await _mediator.Send(getAllOrdersQuery);
                return Ok(response);
            }

            #region Distributed cache

            var objectFromCache = _distributedCache.Get(Orders);
            if(objectFromCache != null)
            {
                var json = Encoding.UTF8.GetString(objectFromCache);
                var result = JsonSerializer.Deserialize<ResponseBuilder<IEnumerable<OrderResponse>>>(json);
                if(result != null)
                {
                    return Ok(result);
                }
            }

            var orders = await _mediator.Send(new GetAllOrdersQuery());

            var serialized = JsonSerializer.SerializeToUtf8Bytes(orders);
            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

            _distributedCache.Set(Orders, serialized, cacheEntryOptions);
            return Ok(orders);

            #endregion 
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<OrderResponse>>> GetById(int id)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery(id));
            if (result.Data == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBuilder<OrderResponse>>> Add(CreateOrderCommand createOrderCommand)
        {
            var result = await _mediator.Send(createOrderCommand);
            if(result.Data == null)
            {
                return BadRequest(result);
            }

            return Created(string.Empty, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<OrderResponse>>> Update(int id, UpdateOrderCommand updateOrderCommand)
        {
            if (id != updateOrderCommand.OrderId)
            {
                return BadRequest($"OrderId does not match with updated data!");
            }

            var result = await _mediator.Send(updateOrderCommand);
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<OrderResponse>>> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteOrderCommand(id));
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
