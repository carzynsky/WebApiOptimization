using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiOptimization.Application.Commands.TerritoryCommands;
using WebApiOptimization.Application.Queries.TerritoryQueries;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerritoryController : ControllerBase
    {
        private IMediator _mediator;

        public TerritoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public ActionResult<ResponseBuilder<IEnumerable<TerritoryResponse>>> GetAll()
        {
            var result = _mediator.Send(new GetAllTerritoriesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseBuilder<TerritoryResponse>> GetById(string id)
        {
            var result = _mediator.Send(new GetTerritoryByIdQuery(id));
            if (result == null)
                return NotFound($"Territory with id={id} not found!");

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<ResponseBuilder<TerritoryResponse>> Add(CreateTerritoryCommand createTerritoryCommand)
        {
            var result = _mediator.Send(createTerritoryCommand);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public ActionResult<ResponseBuilder<TerritoryResponse>> Update(string id, UpdateTerritoryCommand updateTerritoryCommand)
        {
            if (id != updateTerritoryCommand.TerritoryId)
                return BadRequest($"TerritoryId does not match with updated data!");

            var result = _mediator.Send(updateTerritoryCommand);
            if (result == null)
                return NotFound($"Territory with id={id} not found!");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<ResponseBuilder<TerritoryResponse>> Delete(string id)
        {
            var result = _mediator.Send(new DeleteTerritoryCommand(id));
            if (result == null)
                return NotFound($"Territory with id={id} not found!");

            return Ok();
        }
    }
}
