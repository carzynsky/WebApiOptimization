using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        public ShipperController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public ActionResult<ResponseBuilder<IEnumerable<ShipperResponse>>> GetAll()
        {
            var result = _mediator.Send(new GetAllShippersQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ResponseBuilder<ShipperResponse>> GetById(int id)
        {
            var result = _mediator.Send(new GetShipperByIdQuery(id));
            if (result == null)
                return NotFound($"Shipper with id={id} not found!");

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<ResponseBuilder<ShipperResponse>> Add(CreateShipperCommand createShipperCommand)
        {
            var result = _mediator.Send(createShipperCommand);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ResponseBuilder<ShipperResponse>> Update(int id, UpdateShipperCommand updateShipperCommand)
        {
            if (id != updateShipperCommand.ShipperId)
                return BadRequest($"ShipperId does not match with updated data!");

            var result = _mediator.Send(updateShipperCommand);
            if (result == null)
                return NotFound($"Shipper with id={id} not found!");

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ResponseBuilder<ShipperResponse>> Delete(int id)
        {
            var result = _mediator.Send(new DeleteShipperCommand(id));
            if (result == null)
                return NotFound($"Shipper with id={id} not found!");

            return Ok(result);
        }
    }
}
