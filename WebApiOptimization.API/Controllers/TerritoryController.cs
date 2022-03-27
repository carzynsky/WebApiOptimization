using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiOptimization.Application.Commands.Territory;
using WebApiOptimization.Application.Queries.Territory;
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
        public ActionResult<IEnumerable<TerritoryResponse>> GetAll()
        {
            var result = _mediator.Send(new GetAllTerritoriesQuery()).Result;
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<TerritoryResponse> GetById(int id)
        {
            var result = _mediator.Send(new GetTerritoryByIdQuery(id)).Result;
            if (result == null)
                return NotFound($"Territory with id={id} not found!");

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<TerritoryResponse> Add(CreateTerritoryCommand createTerritoryCommand)
        {
            var result = _mediator.Send(createTerritoryCommand);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TerritoryResponse> Update(int id, UpdateTerritoryCommand updateTerritoryCommand)
        {
            int territoryId;
            if (!int.TryParse(updateTerritoryCommand.TerritoryId, out territoryId))
            {
                return BadRequest($"Incorrect territory id!");
            }

            if (id != territoryId)
                return BadRequest($"TerritoryId does not match with updated data!");

            var result = _mediator.Send(updateTerritoryCommand).Result;
            if (result == null)
                return NotFound($"Territory with id={id} not found!");

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<TerritoryResponse> Delete(int id)
        {
            var result = _mediator.Send(new DeleteTerritoryCommand(id)).Result;
            if (result == null)
                return NotFound($"Territory with id={id} not found!");

            return Ok();
        }
    }
}
