using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        private IMediator _mediator;
        public OrderDetailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBuilder<IEnumerable<OrderDetailResponse>>>> Get([FromQuery] GetOrderDetailQuery getOrderDetailQuery)
        {
            var result = await _mediator.Send(getOrderDetailQuery);
            return Ok(result);
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
