using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.CustomerDemographicCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.CustomerDemographicHandlers
{
    public class UpdateCustomerDemographicHandler : IRequestHandler<UpdateCustomerDemographicCommand, ResponseBuilder<CustomerDemographicResponse>>
    {
        private readonly ICustomerDemographicRepository _customerDemographicRepository;

        public UpdateCustomerDemographicHandler(ICustomerDemographicRepository customerDemographicRepository)
        {
            _customerDemographicRepository = customerDemographicRepository;
        }

        public async Task<ResponseBuilder<CustomerDemographicResponse>> Handle(UpdateCustomerDemographicCommand request, CancellationToken cancellationToken)
        {
            var customerDemographicToUpdate = _customerDemographicRepository.GetById(int.Parse(request.CustomerTypeId));
            if(customerDemographicToUpdate == null)
            {
                return new ResponseBuilder<CustomerDemographicResponse> { Message = $"CustomerDemographic with id={request.CustomerTypeId} not found!", Data = null };
            }

            try
            {
                var customerDemographicToUpdateEntity = CustomerDemographicMapper.Mapper.Map<CustomerDemographic>(request);
                _customerDemographicRepository.Update(customerDemographicToUpdateEntity);
                var response = CustomerCustomerDemoMapper.Mapper.Map<CustomerDemographicResponse>(customerDemographicToUpdateEntity);
                return new ResponseBuilder<CustomerDemographicResponse> { Message = "CustomerDemographic updated.", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<CustomerDemographicResponse> { Message = $"CustomerDemographic not updated! Error: {e.InnerException.Message}", Data = null };
            }
        }
    }
}
