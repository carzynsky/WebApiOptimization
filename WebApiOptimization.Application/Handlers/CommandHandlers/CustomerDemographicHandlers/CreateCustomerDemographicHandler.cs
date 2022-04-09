using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.CustomerDemographicCommands;
using WebApiOptimization.Application.Helpers;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.CustomerDemographicHandlers
{
    public class CreateCustomerDemographicHandler : IRequestHandler<CreateCustomerDemographicCommand, ResponseBuilder<CustomerDemographicResponse>>
    {
        private readonly ICustomerDemographicRepository _customerDemographicRepository;

        public CreateCustomerDemographicHandler(ICustomerDemographicRepository customerDemographicRepository)
        {
            _customerDemographicRepository = customerDemographicRepository;
        }

        public async Task<ResponseBuilder<CustomerDemographicResponse>> Handle(CreateCustomerDemographicCommand request, CancellationToken cancellationToken)
        {
            var customerDemographicEntity = CustomerDemographicMapper.Mapper.Map<CustomerDemographic>(request);
            if(customerDemographicEntity == null)
            {
                return new ResponseBuilder<CustomerDemographicResponse> { Message = ResponseBuilderHelper.InvalidData, Data = null };
            }
            try
            {
                var newCustomerDemographic = _customerDemographicRepository.Add(customerDemographicEntity);
                var response = CustomerCustomerDemoMapper.Mapper.Map<CustomerDemographicResponse>(newCustomerDemographic);
                return new ResponseBuilder<CustomerDemographicResponse> { Message = "CustomerDemographic created.", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<CustomerDemographicResponse> { Message = $"CustomerDemographic not created! Error: {e.Message}", Data = null };
            }
            
        }
    }
}
