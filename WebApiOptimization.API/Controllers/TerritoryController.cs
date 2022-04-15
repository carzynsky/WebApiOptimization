using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<ActionResult<ResponseBuilder<IEnumerable<TerritoryResponse>>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllTerritoriesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseBuilder<TerritoryResponse>>> GetById(string id)
        {
            var result = await _mediator.Send(new GetTerritoryByIdQuery(id));
            if (result.Data == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBuilder<TerritoryResponse>>> Add(CreateTerritoryCommand createTerritoryCommand)
        {
            var result = await _mediator.Send(createTerritoryCommand);
            if(result.Data == null)
            {
                return BadRequest(result);
            }

            return Created(string.Empty, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseBuilder<TerritoryResponse>>> Update(string id, UpdateTerritoryCommand updateTerritoryCommand)
        {
            if (id != updateTerritoryCommand.TerritoryId)
            {
                return BadRequest($"TerritoryId does not match with updated data!");
            }

            var result = await _mediator.Send(updateTerritoryCommand);
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseBuilder<TerritoryResponse>>> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteTerritoryCommand(id));
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
