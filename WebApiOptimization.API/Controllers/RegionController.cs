using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.RegionCommands;
using WebApiOptimization.Application.Queries.RegionQueries;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private IMediator _mediator;
        public RegionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBuilder<IEnumerable<RegionResponse>>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllRegionsQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<RegionResponse>>> GetById(int id)
        {
            var result = await _mediator.Send(new GetRegionByIdQuery(id));
            if (result.Data == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBuilder<RegionResponse>>> Add(CreateRegionCommand createRegionCommand)
        {
            var result = await _mediator.Send(createRegionCommand);
            if(result.Data == null)
            {
                return BadRequest(result);
            }

            return Created(string.Empty, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<RegionResponse>>> Update(int id, UpdateRegionCommand updateRegionCommand)
        {
            if (id != updateRegionCommand.RegionId)
            {
                return BadRequest($"RegionId does not match with updated data!");
            }

            var result = await _mediator.Send(updateRegionCommand);
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<RegionResponse>>> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteRegionCommand(id));
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
