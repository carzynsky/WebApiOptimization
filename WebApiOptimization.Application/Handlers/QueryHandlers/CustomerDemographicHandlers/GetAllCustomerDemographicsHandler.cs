using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.CustomerDemographicQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Application.Wrappers;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.CustomerDemographicHandlers
{
    public class GetAllCustomerDemographicsHandler : IRequestHandler<GetAllCustomerDemographicsQuery, ResponseBuilder<IEnumerable<CustomerDemographicResponse>>>
    {
        private readonly ICustomerDemographicRepository _customerDemographicRepository;

        public GetAllCustomerDemographicsHandler(ICustomerDemographicRepository customerDemographicRepository)
        {
            _customerDemographicRepository = customerDemographicRepository;
        }

        public async Task<ResponseBuilder<IEnumerable<CustomerDemographicResponse>>> Handle(GetAllCustomerDemographicsQuery request, CancellationToken cancellationToken)
        {
            List<CustomerDemographic> customerDemographics;
            List<CustomerDemographicResponse> customerDemographicsDto;
            if(request.PageNumber == 0 || request.PageSize == 0)
            {
                customerDemographics = await _customerDemographicRepository.GetAllAsync();
                customerDemographicsDto = CustomerDemographicMapper.Mapper.Map<List<CustomerDemographicResponse>>(customerDemographics);
                return new ResponseBuilder<IEnumerable<CustomerDemographicResponse>> { Message = "OK", Data = customerDemographicsDto };
            }

            customerDemographics = await _customerDemographicRepository.GetAllPagedAsync(request.PageNumber, request.PageSize);
            customerDemographicsDto = CustomerDemographicMapper.Mapper.Map<List<CustomerDemographicResponse>>(customerDemographics);
            return new PagedResponse<IEnumerable<CustomerDemographicResponse>>(customerDemographicsDto, request.PageNumber, request.PageSize, "OK");
        }
    }
}
