using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.CustomerQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Application.Wrappers;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.CustomerHandlers
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
            List<Customer> customers;
            List<CustomerResponse> customersDto;

            if(request.PageNumber == 0 || request.PageSize == 0)
            {
                customers = await _customerRepository.GetAllAsync();
                customersDto = CustomerMapper.Mapper.Map<List<CustomerResponse>>(customers);
                return new ResponseBuilder<IEnumerable<CustomerResponse>> { Message = "OK.", Data = customersDto };
            }

            customers = await _customerRepository.GetAllPagedAsync(request.PageNumber, request.PageSize);
            customersDto = CustomerMapper.Mapper.Map<List<CustomerResponse>>(customers);
            return new PagedResponse<IEnumerable<CustomerResponse>>(customersDto, request.PageNumber, request.PageSize, "OK");
        }
    }
}
