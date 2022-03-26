using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.CustomerCustomerDemo;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.CustomerCustomerDemo
{
    public class GetAllCustomerCustomerDemosHandler : IRequestHandler<GetAllCustomerCustomerDemosQuery, IEnumerable<CustomerCustomerDemoResponse>>
    {
        private readonly ICustomerCustomerDemoRepository _customerCustomerDemoRepository;

        public GetAllCustomerCustomerDemosHandler(ICustomerCustomerDemoRepository customerCustomerDemoRepository)
        {
            _customerCustomerDemoRepository = customerCustomerDemoRepository;
        }

        public async Task<IEnumerable<CustomerCustomerDemoResponse>> Handle(GetAllCustomerCustomerDemosQuery request, CancellationToken cancellationToken)
        {
            var customerCustomerDemos = _customerCustomerDemoRepository.GetAll();
            var response = CustomerCustomerDemoMapper.Mapper.Map<IEnumerable<CustomerCustomerDemoResponse>>(customerCustomerDemos);
            return response;
        }
    }
}
