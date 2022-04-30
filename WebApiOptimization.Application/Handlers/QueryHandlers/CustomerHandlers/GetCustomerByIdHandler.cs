using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.CustomerQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.CustomerHandlers
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, ResponseBuilder<CustomerResponse>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ResponseBuilder<CustomerResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customerEntity = await _customerRepository.GetByIdAsync(request.Id);
            if(customerEntity == null)
            {
                return new ResponseBuilder<CustomerResponse> { Message = $"Customer with id={request.Id} not found!", Data = null };
            }

            var response = CustomerMapper.Mapper.Map<CustomerResponse>(customerEntity);
            return new ResponseBuilder<CustomerResponse> { Message = "OK.", Data = response };
        }
    }
}
