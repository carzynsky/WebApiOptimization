using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.SupplierCommands;
using WebApiOptimization.Application.Queries.SupplierQueries;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private IMediator _mediator;

        public SupplierController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBuilder<IEnumerable<SupplierResponse>>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllSuppliersQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<SupplierResponse>>> GetById(int id)
        {
            var result = await _mediator.Send(new GetSupplierByIdQuery(id));
            if (result.Data == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBuilder<SupplierResponse>>> Add(CreateSupplierCommand createSupplierCommand)
        {
            var result = await _mediator.Send(createSupplierCommand);
            if(result.Data == null)
            {
                return BadRequest(result);
            }

            return Created(string.Empty, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<SupplierResponse>>> Update(int id, UpdateSupplierCommand updateSupplierCommand)
        {
            if (id != updateSupplierCommand.SupplierId)
            {
                return BadRequest($"SupplierId does not match with updated data!");
            }

            var result = await _mediator.Send(updateSupplierCommand);
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<SupplierResponse>>> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteSupplierCommand(id));
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
