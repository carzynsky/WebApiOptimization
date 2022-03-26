using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.CustomerCustomerDemo;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.CustomerCustomerDemo
{
    public class GetCustomerCustomerDemoByIdHandler : IRequestHandler<GetCustomerCustomerDemoByIdQuery, CustomerCustomerDemoResponse>
    {
        private readonly ICustomerCustomerDemoRepository _customerCustomerDemoRepository;

        public GetCustomerCustomerDemoByIdHandler(ICustomerCustomerDemoRepository customerCustomerDemoRepository)
        {
            _customerCustomerDemoRepository = customerCustomerDemoRepository;
        }
        public async Task<CustomerCustomerDemoResponse> Handle(GetCustomerCustomerDemoByIdQuery request, CancellationToken cancellationToken)
        {
            var customerCustomerDemoEntity = _customerCustomerDemoRepository.GetById(request.Id);
            var response = CustomerCustomerDemoMapper.Mapper.Map<CustomerCustomerDemoResponse>(customerCustomerDemoEntity);
            return response;
        }
    }
}
