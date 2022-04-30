using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.CustomerCommands;
using WebApiOptimization.Application.Queries.CustomerQueries;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDistributedCache _distributedCache;
        public string CustomersKey => "Customers";

        public CustomerController(IMediator mediator, IDistributedCache distributedCache)
        {
            _mediator = mediator;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBuilder<IEnumerable<CustomerResponse>>>> GetAll([FromQuery] GetAllCustomersQuery getAllCustomersQuery)
        {
            if(getAllCustomersQuery.PageNumber != 0 && getAllCustomersQuery.PageSize != 0)
            {
                var result = await _mediator.Send(getAllCustomersQuery);
                return Ok(result);
            }

            #region Distributed cache

            var objectFromCache = _distributedCache.Get(CustomersKey);
            if (objectFromCache != null)
            {
                var json = Encoding.UTF8.GetString(objectFromCache);
                var result = JsonSerializer.Deserialize<ResponseBuilder<IEnumerable<CustomerResponse>>>(json);
                if (result != null)
                {
                    return Ok(result);
                }
            }

            var customers = await _mediator.Send(getAllCustomersQuery);
            var serialized = JsonSerializer.SerializeToUtf8Bytes(customers);
            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

            _distributedCache.Set(CustomersKey, serialized, cacheEntryOptions);
            return Ok(customers);

            #endregion
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseBuilder<CustomerResponse>>> GetById(string id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery(id));
            if (result.Data == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBuilder<CustomerResponse>>> Add(CreateCustomerCommand createCustomerCommand)
        {
            var result = await _mediator.Send(createCustomerCommand);
            if(result.Data == null)
            {
                return BadRequest(result);
            }

            return Created(string.Empty, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseBuilder<CustomerResponse>>> Update(string id, UpdateCustomerCommand updateCustomerCommand)
        {
            if (id != updateCustomerCommand.CustomerID)
            {
                return BadRequest($"CustomerId does not match with updated data!");
            }

            var result = await _mediator.Send(updateCustomerCommand);
            if (result.Data == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseBuilder<CustomerResponse>>> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand(id));
            if (result.Data == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
