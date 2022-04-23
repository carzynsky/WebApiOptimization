using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.CustomerDemographicQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.CustomerDemographicHandlers
{
    public class GetCustomerDemographicByIdHandler : IRequestHandler<GetCustomerDemographicByIdQuery, ResponseBuilder<CustomerDemographicResponse>>
    {
        private readonly ICustomerDemographicRepository _customerDemographicRepository;

        public GetCustomerDemographicByIdHandler(ICustomerDemographicRepository customerDemographicRepository)
        {
            _customerDemographicRepository = customerDemographicRepository;
        }

        public async Task<ResponseBuilder<CustomerDemographicResponse>> Handle(GetCustomerDemographicByIdQuery request, CancellationToken cancellationToken)
        {
            var customerDemographicEntity = await _customerDemographicRepository.GetByCustomerTypeIdAsync(request.CustomerTypeId.ToString());
            if(customerDemographicEntity == null)
            {
                return new ResponseBuilder<CustomerDemographicResponse> { Message = $"CustomerDemographic with customer type id={request.CustomerTypeId} not found!", Data = null };
            }

            var response = CustomerDemographicMapper.Mapper.Map<CustomerDemographicResponse>(customerDemographicEntity);
            return new ResponseBuilder<CustomerDemographicResponse> { Message = "OK", Data = response };
        }
    }
}
