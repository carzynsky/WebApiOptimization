﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        private IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBuilder<IEnumerable<OrderResponse>>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllOrdersQuery());
            return Ok(result);
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
