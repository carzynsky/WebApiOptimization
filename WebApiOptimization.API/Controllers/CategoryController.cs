using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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
        private readonly IDistributedCache _distributedCache;
        public string CategoriesKey => "Categories";

        public CategoryController(IMediator mediator, IDistributedCache distributedCache)
        {
            _mediator = mediator;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBuilder<IEnumerable<CategoryResponse>>>> GetAll([FromQuery] GetAllCategoriesQuery getAllCategoriesQuery)
        {
            if(getAllCategoriesQuery.PageNumber != 0 && getAllCategoriesQuery.PageSize != 0)
            {
                var result = await _mediator.Send(getAllCategoriesQuery);
                return Ok(result);
            }

            #region Distributed cache

            var objectFromCache = _distributedCache.Get(CategoriesKey);
            if (objectFromCache != null)
            {
                var json = Encoding.UTF8.GetString(objectFromCache);
                var result = JsonSerializer.Deserialize<ResponseBuilder<IEnumerable<CategoryResponse>>>(json);
                if (result != null)
                {
                    return Ok(result);
                }
            }

            var categories = await _mediator.Send(getAllCategoriesQuery);
            var serialized = JsonSerializer.SerializeToUtf8Bytes(categories);
            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

            _distributedCache.Set(CategoriesKey, serialized, cacheEntryOptions);
            return Ok(categories);

            #endregion
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<CategoryResponse>>> GetById(int id)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(id));
            if (result.Data == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBuilder<CategoryResponse>>> Add(CreateCategoryCommand createCategoryCommand)
        {
            var result = await _mediator.Send(createCategoryCommand);
            if(result.Data == null)
            {
                return BadRequest(result);
            }

            return Created(string.Empty, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<CategoryResponse>>> Update(int id, UpdateCategoryCommand updateCategoryCommand)
        {
            if (id != updateCategoryCommand.CategoryId)
                return BadRequest($"CategoryId does not match with updated data!");

            var result = await _mediator.Send(updateCategoryCommand);
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<CategoryResponse>>> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(id));
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
