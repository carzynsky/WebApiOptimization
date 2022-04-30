using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.OrderDetailCommands;
using WebApiOptimization.Application.Queries.OrderDetailQueries;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDistributedCache _distributedCache;
        public string OrderDetailsKey => "OrderDetails";

        public OrderDetailController(IMediator mediator, IDistributedCache distributedCache)
        {
            _mediator = mediator;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBuilder<IEnumerable<OrderDetailResponse>>>> Get([FromQuery] GetOrderDetailQuery getOrderDetailQuery)
        {
            if(getOrderDetailQuery.PageNumber != 0 && getOrderDetailQuery.PageSize != 0)
            {
                var response = await _mediator.Send(getOrderDetailQuery);
                return Ok(response);
            }

            #region Distributed cache

            var objectFromCache = _distributedCache.Get(OrderDetailsKey);
            if (objectFromCache != null)
            {
                var json = Encoding.UTF8.GetString(objectFromCache);
                var result = JsonSerializer.Deserialize<ResponseBuilder<IEnumerable<OrderDetailResponse>>>(json);
                if (result != null)
                {
                    return Ok(result);
                }
            }

            var orderDetails = await _mediator.Send(getOrderDetailQuery);

            var serialized = JsonSerializer.SerializeToUtf8Bytes(orderDetails);
            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

            _distributedCache.Set(OrderDetailsKey, serialized, cacheEntryOptions);
            return Ok(orderDetails);

            #endregion
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBuilder<OrderDetailResponse>>> Add(CreateOrderDetailCommand createOrderDetailCommand)
        {
            var result = await _mediator.Send(createOrderDetailCommand);
            if(result.Data == null)
            {
                return BadRequest(result);
            }

            return Created(string.Empty, result);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseBuilder<OrderDetailResponse>>> Update([FromQuery] UpdateOrderDetailQueryParameter updateOrderDetailQueryParameter, UpdateOrderDetailCommand updateOrderDetailCommand)
        {
            if (updateOrderDetailQueryParameter.OrderId != updateOrderDetailCommand.OrderId)
            {
                return BadRequest($"OrderId does not match with updated data!");
            }

            if (updateOrderDetailQueryParameter.ProductId != updateOrderDetailCommand.ProductId)
            {
                return BadRequest($"ProductId does not match with updated data!");
            }

            var result = await _mediator.Send(updateOrderDetailCommand);
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Allow deleting only by OrderID!
        /// </summary>
        /// <param name="deleteOrderDetailCommand"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<ResponseBuilder<List<OrderDetailResponse>>>> Delete([FromQuery] DeleteOrderDetailCommand deleteOrderDetailCommand)
        {
            var result = await _mediator.Send(deleteOrderDetailCommand);
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
