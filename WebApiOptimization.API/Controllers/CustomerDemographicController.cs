using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.CustomerDemographicCommands;
using WebApiOptimization.Application.Queries.CustomerDemographicQueries;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDemographicController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDistributedCache _distributedCache;
        public string CustomerDemographicsKey => "CustomerDemographics";

        public CustomerDemographicController(IMediator mediator, IDistributedCache distributedCache)
        {
            _mediator = mediator;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBuilder<IEnumerable<CustomerDemographicResponse>>>> GetAll([FromQuery] GetAllCustomerDemographicsQuery getAllCustomerDemographicsQuery)
        {
            if(getAllCustomerDemographicsQuery.PageNumber != 0 && getAllCustomerDemographicsQuery.PageSize != 0)
            {
                var result = await _mediator.Send(getAllCustomerDemographicsQuery);
                return Ok(result);
            }

            #region Distributed cache

            var objectFromCache = _distributedCache.Get(CustomerDemographicsKey);
            if (objectFromCache != null)
            {
                var json = Encoding.UTF8.GetString(objectFromCache);
                var result = JsonSerializer.Deserialize<ResponseBuilder<IEnumerable<CustomerDemographicResponse>>>(json);
                if (result != null)
                {
                    return Ok(result);
                }
            }

            var customerDemographics = await _mediator.Send(getAllCustomerDemographicsQuery);
            var serialized = JsonSerializer.SerializeToUtf8Bytes(customerDemographics);
            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

            _distributedCache.Set(CustomerDemographicsKey, serialized, cacheEntryOptions);
            return Ok(customerDemographics);

            #endregion
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<CustomerDemographicResponse>>> GetById(int id)
        {
            var result = await _mediator.Send(new GetCustomerDemographicByIdQuery(id));
            if (result.Data == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBuilder<CustomerDemographicResponse>>> Add(CreateCustomerDemographicCommand createCustomerDemographicCommand)
        {
            var result = await _mediator.Send(createCustomerDemographicCommand);
            if(result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ResponseBuilder<CustomerDemographicResponse>>> Update(int id, UpdateCustomerDemographicCommand updateCustomerDemographicCommand)
        {
            int customerTypeId;
            if(!int.TryParse(updateCustomerDemographicCommand.CustomerTypeId, out customerTypeId))
            {
                return BadRequest($"Incorrect format of customer type id!");
            }

            if (id != customerTypeId)
            {
                return BadRequest($"CustomerTypeId does not match with updated data!");
            }

            var result = await _mediator.Send(updateCustomerDemographicCommand);
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseBuilder<CustomerDemographicResponse>>> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteCustomerDemographicCommand(id));
            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
