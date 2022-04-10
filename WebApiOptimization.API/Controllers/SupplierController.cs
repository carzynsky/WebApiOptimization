using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public ActionResult<ResponseBuilder<IEnumerable<SupplierResponse>>> GetAll()
        {
            var result = _mediator.Send(new GetAllSuppliersQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ResponseBuilder<SupplierResponse>> GetById(int id)
        {
            var result = _mediator.Send(new GetSupplierByIdQuery(id));
            if (result == null)
                return NotFound($"Supplier with id={id} not found!");

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<ResponseBuilder<SupplierResponse>> Add(CreateSupplierCommand createSupplierCommand)
        {
            var result = _mediator.Send(createSupplierCommand);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ResponseBuilder<SupplierResponse>> Update(int id, UpdateSupplierCommand updateSupplierCommand)
        {
            if (id != updateSupplierCommand.SupplierId)
                return BadRequest($"SupplierId does not match with updated data!");

            var result = _mediator.Send(updateSupplierCommand);
            if (result == null)
                return NotFound($"Supplier with id={id} not found!");

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ResponseBuilder<SupplierResponse>> Delete(int id)
        {
            var result = _mediator.Send(new DeleteSupplierCommand(id));
            if (result == null)
                return NotFound($"Supplier with id={id} not found!");

            return Ok(result);
        }
    }
}
