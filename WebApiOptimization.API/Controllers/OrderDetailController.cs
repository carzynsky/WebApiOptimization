using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiOptimization.Application.Commands.OrderDetail;
using WebApiOptimization.Application.Queries.OrderDetail;
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
        public ActionResult<IEnumerable<OrderDetailResponse>> GetAll()
        {
            var result = _mediator.Send(new GetAllOrderDetailsQuery()).Result;
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<OrderDetailResponse> GetById(int id)
        {
            var result = _mediator.Send(new GetOrderDetailByIdQuery(id)).Result;
            if (result == null)
                return NotFound($"OrderDetail with order id={id} not found!");

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<OrderDetailResponse> Add(CreateOrderDetailCommand createOrderDetailCommand)
        {
            var result = _mediator.Send(createOrderDetailCommand);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<OrderDetailResponse> Update(int id, UpdateOrderDetailCommand updateOrderDetailCommand)
        {
            if (id != updateOrderDetailCommand.OrderId)
                return BadRequest($"OrderId does not match with updated data!");

            var result = _mediator.Send(updateOrderDetailCommand).Result;
            if (result == null)
                return NotFound($"OrderDetail with order id={id} not found!");

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<OrderDetailResponse> Delete(int id)
        {
            var result = _mediator.Send(new DeleteOrderDetailCommand(id)).Result;
            if (result == null)
                return NotFound($"OrderDetail with order id={id} not found!");

            return Ok();
        }
    }
}
