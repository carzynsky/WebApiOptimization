using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiOptimization.Application.Commands.ProductCommands;
using WebApiOptimization.Application.Queries.ProductQueries;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public ActionResult<ResponseBuilder<IEnumerable<ProductResponse>>> GetAll()
        {
            var result = _mediator.Send(new GetAllProductsQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ResponseBuilder<ProductResponse>> GetById(int id)
        {
            var result = _mediator.Send(new GetProductByIdQuery(id));
            if (result == null)
                return NotFound($"Product with id={id} not found!");

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<ResponseBuilder<ProductResponse>> Add(CreateProductCommand createProductCommand)
        {
            var result = _mediator.Send(createProductCommand);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ResponseBuilder<ProductResponse>> Update(int id, UpdateProductCommand updateProductCommand)
        {
            if (id != updateProductCommand.ProductId)
                return BadRequest($"ProductId does not match with updated data!");

            var result = _mediator.Send(updateProductCommand);
            if (result == null)
                return NotFound($"Product with id={id} not found!");

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ResponseBuilder<ProductResponse>> Delete(int id)
        {
            var result = _mediator.Send(new DeleteProductCommand(id));
            if (result == null)
                return NotFound($"Product with id={id} not found!");

            return Ok(result);
        }
    }
}
