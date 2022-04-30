using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
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
        private readonly IMediator _mediator;
        private readonly IDistributedCache _distributedCache;
        public string SuppliersKey => "Suppliers";

        public SupplierController(IMediator mediator, IDistributedCache distributedCache)
        {
            _mediator = mediator;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBuilder<IEnumerable<SupplierResponse>>>> GetAll([FromQuery] GetAllSuppliersQuery getAllSuppliersQuery)
        {
            if(getAllSuppliersQuery.PageNumber != 0 && getAllSuppliersQuery.PageSize != 0)
            {
                var result = await _mediator.Send(getAllSuppliersQuery);
                return Ok(result);
            }

            #region Distributed cache

            var objectFromCache = _distributedCache.Get(SuppliersKey);
            if (objectFromCache != null)
            {
                var json = Encoding.UTF8.GetString(objectFromCache);
                var result = JsonSerializer.Deserialize<ResponseBuilder<IEnumerable<SupplierResponse>>>(json);
                if (result != null)
                {
                    return Ok(result);
                }
            }

            var suppliers = await _mediator.Send(getAllSuppliersQuery);
            var serialized = JsonSerializer.SerializeToUtf8Bytes(suppliers);
            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

            _distributedCache.Set(SuppliersKey, serialized, cacheEntryOptions);
            return Ok(suppliers);

            #endregion 
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
