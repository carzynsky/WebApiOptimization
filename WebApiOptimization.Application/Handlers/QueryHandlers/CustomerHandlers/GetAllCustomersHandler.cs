using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.CustomerQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.Customer
{
    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, ResponseBuilder<IEnumerable<CustomerResponse>>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetAllCustomersHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ResponseBuilder<IEnumerable<CustomerResponse>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllAsync();
            var response = CustomerMapper.Mapper.Map<IEnumerable<CustomerResponse>>(customers);
            return new ResponseBuilder<IEnumerable<CustomerResponse>> { Message = "OK.", Data = response };
        }
    }
}
