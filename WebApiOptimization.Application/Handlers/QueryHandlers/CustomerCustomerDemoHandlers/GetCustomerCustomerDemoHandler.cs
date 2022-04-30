using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.CustomerCustomerDemoQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Application.Wrappers;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.CustomerCustomerDemoHandlers
{
    public class GetCustomerCustomerDemoHandler : IRequestHandler<GetCustomerCustomerDemoQuery, ResponseBuilder<IEnumerable<CustomerCustomerDemoResponse>>>
    {
        private readonly ICustomerCustomerDemoRepository _customerCustomerDemoRepository;

        public GetCustomerCustomerDemoHandler(ICustomerCustomerDemoRepository customerCustomerDemoRepository)
        {
            _customerCustomerDemoRepository = customerCustomerDemoRepository;
        }
        public async Task<ResponseBuilder<IEnumerable<CustomerCustomerDemoResponse>>> Handle(GetCustomerCustomerDemoQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<CustomerCustomerDemo> customerCustomerDemos;

            if(request.CustomerId != null && request.CustomerTypeId == null)
            {
                customerCustomerDemos = await _customerCustomerDemoRepository.GetByCustomerIdAsync(request.CustomerId, true);
            }
            else if(request.CustomerId == null && request.CustomerTypeId != null)
            {
                customerCustomerDemos = await _customerCustomerDemoRepository.GetByCustomerTypeIdAsync(request.CustomerTypeId, true);
            }
            else if(request.CustomerId != null && request.CustomerTypeId != null)
            {
                customerCustomerDemos = await _customerCustomerDemoRepository.GetByCustomerIdAndCustomerTypeIdAsync(request.CustomerId, request.CustomerTypeId, true);
            }
            else if(request.PageNumber != 0 && request.PageSize != 0)
            {
                customerCustomerDemos = await _customerCustomerDemoRepository.GetAllPagedAsync(request.PageNumber, request.PageSize);
                var customerCustomerDemosDto = CustomerCustomerDemoMapper.Mapper.Map<IEnumerable<CustomerCustomerDemoResponse>>(customerCustomerDemos);
                return new PagedResponse<IEnumerable<CustomerCustomerDemoResponse>>(customerCustomerDemosDto, request.PageNumber, request.PageSize, "OK");
            }
            else
            {
                customerCustomerDemos = await _customerCustomerDemoRepository.GetAllAsync();
            }

            var response = CustomerCustomerDemoMapper.Mapper.Map<IEnumerable<CustomerCustomerDemoResponse>>(customerCustomerDemos);
            return new ResponseBuilder<IEnumerable<CustomerCustomerDemoResponse>> { Message = "OK.", Data = response };
        }
    }
}
