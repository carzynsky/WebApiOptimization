using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.ProductCommands;
using WebApiOptimization.Application.Queries.ProductQueries;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDistributedCache _distributedCache;
        public string ProductsKey => "Products";

        public ProductController(IMediator mediator, IDistributedCache distributedCache)
        {
            _mediator = mediator;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBuilder<IEnumerable<ProductResponse>>>> GetAll([FromQuery] GetAllProductsQuery getAllProductsQuery)
        {
            if(getAllProductsQuery.PageNumber != 0 && getAllProductsQuery.PageSize != 0)
            {
                var result = await _mediator.Send(getAllProductsQuery);
                return Ok(result);
            }

            #region Distributed cache

            var objectFromCache = _distributedCache.Get(ProductsKey);
            if (objectFromCache != null)
            {
                var json = Encoding.UTF8.GetString(objectFromCache);
                var result = JsonSerializer.Deserialize<ResponseBuilder<IEnumerable<ProductResponse>>>(json);
                if (result != null)
                {
                    return Ok(result);
                }
            }

            var products = await _mediator.Send(new GetAllProductsQuery());
            var serialized = JsonSerializer.SerializeToUtf8Bytes(products);
            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

            _distributedCache.Set(ProductsKey, serialized, cacheEntryOptions);
            return Ok(products);

            #endregion
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<ProductResponse>>> GetById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBuilder<ProductResponse>>> Add(CreateProductCommand createProductCommand)
        {
            var result = await _mediator.Send(createProductCommand);
            if(result.Data == null)
            {
                return BadRequest(result);
            }

            return Created(string.Empty, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<ProductResponse>>> Update(int id, UpdateProductCommand updateProductCommand)
        {
            if (id != updateProductCommand.ProductId)
            {
                return BadRequest($"ProductId does not match with updated data!");
            }

            var result = await _mediator.Send(updateProductCommand);
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<ProductResponse>>> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
