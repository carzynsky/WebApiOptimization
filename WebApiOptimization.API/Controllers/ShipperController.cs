using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiOptimization.Application.Commands.Shipper;
using WebApiOptimization.Application.Queries.Shipper;
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
        public ActionResult<IEnumerable<ShipperResponse>> GetAll()
        {
            var result = _mediator.Send(new GetAllShippersQuery()).Result;
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ShipperResponse> GetById(int id)
        {
            var result = _mediator.Send(new GetShipperByIdQuery(id)).Result;
            if (result == null)
                return NotFound($"Shipper with id={id} not found!");

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<ShipperResponse> Add(CreateShipperCommand createShipperCommand)
        {
            var result = _mediator.Send(createShipperCommand);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ShipperResponse> Update(int id, UpdateShipperCommand updateShipperCommand)
        {
            if (id != updateShipperCommand.ShipperId)
                return BadRequest($"ShipperId does not match with updated data!");

            var result = _mediator.Send(updateShipperCommand).Result;
            if (result == null)
                return NotFound($"Shipper with id={id} not found!");

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ShipperResponse> Delete(int id)
        {
            var result = _mediator.Send(new DeleteShipperCommand(id)).Result;
            if (result == null)
                return NotFound($"Shipper with id={id} not found!");

            return Ok();
        }
    }
}
