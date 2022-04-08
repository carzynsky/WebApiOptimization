﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiOptimization.Application.Commands.Region;
using WebApiOptimization.Application.Queries.Region;
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
        public ActionResult<IEnumerable<RegionResponse>> GetAll()
        {
            var result = _mediator.Send(new GetAllRegionsQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<RegionResponse> GetById(int id)
        {
            var result = _mediator.Send(new GetRegionByIdQuery(id));
            if (result == null)
                return NotFound($"Region with id={id} not found!");

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<RegionResponse> Add(CreateRegionCommand createRegionCommand)
        {
            var result = _mediator.Send(createRegionCommand);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<RegionResponse> Update(int id, UpdateRegionCommand updateRegionCommand)
        {
            if (id != updateRegionCommand.RegionId)
                return BadRequest($"RegionId does not match with updated data!");

            var result = _mediator.Send(updateRegionCommand);
            if (result == null)
                return NotFound($"Region with id={id} not found!");

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<RegionResponse> Delete(int id)
        {
            var result = _mediator.Send(new DeleteRegionCommand(id));
            if (result == null)
                return NotFound($"Region with id={id} not found!");

            return Ok();
        }
    }
}
