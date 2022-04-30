using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.CustomerCustomerDemoCommands;
using WebApiOptimization.Application.Queries.CustomerCustomerDemoQueries;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerCustomerDemoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDistributedCache _distributedCache;
        public string CustomerCustomerDemosKey => "CustomerCustomerDemos";

        public CustomerCustomerDemoController(IMediator mediator, IDistributedCache distributedCache)
        {
            _mediator = mediator;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBuilder<IEnumerable<CustomerCustomerDemoResponse>>>> Get([FromQuery] GetCustomerCustomerDemoQuery getCustomerCustomerDemoQuery)
        {
            if(getCustomerCustomerDemoQuery.PageNumber != 0 & getCustomerCustomerDemoQuery.PageSize != 0)
            {
                var result = await _mediator.Send(getCustomerCustomerDemoQuery);
                return Ok(result);
            }

            #region Distributed cache

            var objectFromCache = _distributedCache.Get(CustomerCustomerDemosKey);
            if (objectFromCache != null)
            {
                var json = Encoding.UTF8.GetString(objectFromCache);
                var result = JsonSerializer.Deserialize<ResponseBuilder<IEnumerable<CustomerCustomerDemoResponse>>>(json);
                if (result != null)
                {
                    return Ok(result);
                }
            }

            var customerCustomerDemos = await _mediator.Send(getCustomerCustomerDemoQuery);
            var serialized = JsonSerializer.SerializeToUtf8Bytes(customerCustomerDemos);
            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

            _distributedCache.Set(CustomerCustomerDemosKey, serialized, cacheEntryOptions);
            return Ok(customerCustomerDemos);

            #endregion 
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBuilder<CustomerCustomerDemoResponse>>> Add(CreateCustomerCustomerDemoCommand createCustomerCustomerDemoCommand)
        {
            var result = await _mediator.Send(createCustomerCustomerDemoCommand);
            if(result.Data == null)
            {
                return BadRequest(result);
            }

            return Created(string.Empty, result);
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseBuilder<CustomerCustomerDemoResponse>>> Delete([FromQuery] DeleteCustomerCustomerDemoCommand deleteCustomerCustomerDemoCommand)
        {
            if(deleteCustomerCustomerDemoCommand.CustomerId == null && deleteCustomerCustomerDemoCommand.CustomerTypeId == null)
            {
                return BadRequest("Parameters not provided!");
            }

            var result = await _mediator.Send(deleteCustomerCustomerDemoCommand);
            if (result.Data == null)
            {
                return NotFound(result);
            }
            
            return Ok(result);
        }
    }
}
