using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.CustomerDemographicQueries;
using WebApiOptimization.Application.Responses;
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
            var customerDemographics = _customerDemographicRepository.GetAll();
            var response = CustomerDemographicMapper.Mapper.Map<IEnumerable<CustomerDemographicResponse>>(customerDemographics);
            return new ResponseBuilder<IEnumerable<CustomerDemographicResponse>> { Message = "OK", Data = response };
        }
    }
}
