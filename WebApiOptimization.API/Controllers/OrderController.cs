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
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;

        public OrderController(IMediator mediator, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            _mediator = mediator;
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBuilder<IEnumerable<OrderResponse>>>> GetAll()
        {
            // InMemory Cache
            ResponseBuilder<IEnumerable<OrderResponse>> response;
            if (!_memoryCache.TryGetValue("OrdersKey", out response))
            {
                // setting cache options
                var cacheEntryOptions = new MemoryCacheEntryOptions()

                // keep in cache for 15 seconds since last access
                .SetSlidingExpiration(TimeSpan.FromSeconds(15));
                response = await _mediator.Send(new GetAllOrdersQuery());

                // Save data in cache
                _memoryCache.Set("OrdersKey", response, cacheEntryOptions);
            }

            return Ok(response);
            

            // sql server distributed caching but it takes too much time tho
            /*
            var orders = await GetOrdersFromCache();
            if(orders != null)
            {
                return Ok(orders);
            }
            await SetOrdersCache();
            return Ok(await GetOrdersFromCache());
            */
        }
        
        // Takes so much time
        /*
        private async Task<ResponseBuilder<IEnumerable<OrderResponse>>> GetOrdersFromCache()
        {
            var orders = await _distributedCache.GetAsync("Orders");
            if(orders != null)
            {
                var ordersStr = Encoding.UTF8.GetString(orders);
                var ordersObj = JsonSerializer.Deserialize<ResponseBuilder<IEnumerable<OrderResponse>>>(ordersStr);
                return ordersObj;
            }
            return null;
        }

        private async Task SetOrdersCache()
        {
            var orders = await _mediator.Send(new GetAllOrdersQuery());
            byte[] ordersObjValue = JsonSerializer.SerializeToUtf8Bytes(orders);
            await _distributedCache.SetAsync("Orders", ordersObjValue, new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(15)));
        }
        */

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
