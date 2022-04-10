using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiOptimization.Application.Commands.CategoryCommands;
using WebApiOptimization.Application.Queries.CategoryQueries;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public ActionResult<ResponseBuilder<IEnumerable<CategoryResponse>>> GetAll()
        {
            var result = _mediator.Send(new GetAllCategoriesQuery());
            
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ResponseBuilder<CategoryResponse>> GetById(int id)
        {
            var result = _mediator.Send(new GetCategoryByIdQuery(id));
            if (result == null)
                return NotFound($"Category with id={id} not found!");

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<ResponseBuilder<CategoryResponse>> Add(CreateCategoryCommand createCategoryCommand)
        {
            var result = _mediator.Send(createCategoryCommand);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ResponseBuilder<CategoryResponse>> Update(int id, UpdateCategoryCommand updateCategoryCommand)
        {
            if (id != updateCategoryCommand.CategoryId)
                return BadRequest($"CategoryId does not match with updated data!");

            var result = _mediator.Send(updateCategoryCommand);
            if (result == null)
                return NotFound($"Category with id={id} not found!");

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ResponseBuilder<CategoryResponse>> Delete(int id)
        {
            var result = _mediator.Send(new DeleteCategoryCommand(id));
            if (result == null)
                return NotFound($"Category with id={id} not found!");

            return Ok(result);
        }
    }
}
