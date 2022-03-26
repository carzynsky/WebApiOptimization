using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiOptimization.Application.Commands.Supplier;
using WebApiOptimization.Application.Queries.Supplier;
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
        public ActionResult<IEnumerable<SupplierResponse>> GetAll()
        {
            var result = _mediator.Send(new GetAllSuppliersQuery()).Result;
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<SupplierResponse> GetById(int id)
        {
            var result = _mediator.Send(new GetSupplierByIdQuery(id)).Result;
            if (result == null)
                return NotFound($"Supplier with id={id} not found!");

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<SupplierResponse> Add(CreateSupplierCommand createSupplierCommand)
        {
            var result = _mediator.Send(createSupplierCommand);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<SupplierResponse> Update(int id, UpdateSupplierCommand updateSupplierCommand)
        {
            if (id != updateSupplierCommand.SupplierId)
                return BadRequest($"SupplierId does not match with updated data!");

            var result = _mediator.Send(updateSupplierCommand).Result;
            if (result == null)
                return NotFound($"Supplier with id={id} not found!");

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<SupplierResponse> Delete(int id)
        {
            var result = _mediator.Send(new DeleteSupplierCommand(id)).Result;
            if (result == null)
                return NotFound($"Supplier with id={id} not found!");

            return Ok();
        }
    }
}
