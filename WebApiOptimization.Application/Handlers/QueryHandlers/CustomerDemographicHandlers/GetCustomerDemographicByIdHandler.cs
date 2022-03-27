using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.CustomerDemographic;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.CustomerDemographicHandlers
{
    public class GetCustomerDemographicByIdHandler : IRequestHandler<GetCustomerDemographicByIdQuery, CustomerDemographicResponse>
    {
        private readonly ICustomerDemographicRepository _customerDemographicRepository;
        public GetCustomerDemographicByIdHandler(ICustomerDemographicRepository customerDemographicRepository)
        {
            _customerDemographicRepository = customerDemographicRepository;
        }
        public async Task<CustomerDemographicResponse> Handle(GetCustomerDemographicByIdQuery request, CancellationToken cancellationToken)
        {
            var customerDemographicEntity = _customerDemographicRepository.GetByCustomerTypeId(request.CustomerTypeId.ToString());
            var response = CustomerDemographicMapper.Mapper.Map<CustomerDemographicResponse>(customerDemographicEntity);
            return response;
        }
    }
}
