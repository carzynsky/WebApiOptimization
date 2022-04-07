using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiOptimization.Application.Commands.Order;
using WebApiOptimization.Application.Queries.Order;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public ActionResult<List<OrderResponse>> GetAll()
        {
            var result = _mediator.Send(new GetAllOrdersQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<OrderResponse> GetById(int id)
        {
            var result = _mediator.Send(new GetOrderByIdQuery(id)).Result;
            if (result == null)
                return NotFound($"Order with id={id} not found!");

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<OrderResponse> Add(CreateOrderCommand createOrderCommand)
        {
            var result = _mediator.Send(createOrderCommand);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<OrderResponse> Update(int id, UpdateOrderCommand updateOrderCommand)
        {
            if (id != updateOrderCommand.OrderId)
                return BadRequest($"OrderId does not match with updated data!");

            var result = _mediator.Send(updateOrderCommand).Result;
            if (result == null)
                return NotFound($"Order with id={id} not found!");

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<OrderResponse> Delete(int id)
        {
            var result = _mediator.Send(new DeleteOrderCommand(id)).Result;
            if (result == null)
                return NotFound($"Order with id={id} not found!");

            return Ok();
        }
    }
}
