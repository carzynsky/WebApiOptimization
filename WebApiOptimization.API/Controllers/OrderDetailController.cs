using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiOptimization.Application.Commands.OrderDetail;
using WebApiOptimization.Application.Queries.OrderDetail;
using WebApiOptimization.Application.Queries.OrderDetail.Queries;
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
        public ActionResult<ResponseBuilder<OrderDetailResponse>> Get([FromQuery] GetOrderDetailQuery getOrderDetailQuery)
        {
            var result = _mediator.Send(getOrderDetailQuery);
            if (result == null)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<ResponseBuilder<OrderDetailResponse>> Add(CreateOrderDetailCommand createOrderDetailCommand)
        {
            var result = _mediator.Send(createOrderDetailCommand);
            return Ok(result);
        }

        [HttpPut]
        public ActionResult<ResponseBuilder<OrderDetailResponse>> Update([FromQuery] UpdateOrderDetailQueryParameter updateOrderDetailQueryParameter, UpdateOrderDetailCommand updateOrderDetailCommand)
        {
            if (updateOrderDetailQueryParameter.OrderId != updateOrderDetailCommand.OrderId)
                return BadRequest($"OrderId does not match with updated data!");

            if (updateOrderDetailQueryParameter.ProductId != updateOrderDetailCommand.ProductId)
                return BadRequest($"ProductId does not match with updated data!");

            var result = _mediator.Send(updateOrderDetailCommand);
            if (result == null)
                return NotFound($"OrderDetail with order id={updateOrderDetailQueryParameter.OrderId} and product id={updateOrderDetailQueryParameter.ProductId} not found!");

            return Ok(result);
        }

        /// <summary>
        /// Allow deleting only by OrderID!
        /// </summary>
        /// <param name="deleteOrderDetailCommand"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult<List<ResponseBuilder<OrderDetailResponse>>> Delete([FromQuery] DeleteOrderDetailCommand deleteOrderDetailCommand)
        {
            var result = _mediator.Send(deleteOrderDetailCommand);
            if (result == null)
                return NotFound($"OrderDetails with order id={deleteOrderDetailCommand.OrderID} not found!");

            return Ok(result);
        }
    }
}
