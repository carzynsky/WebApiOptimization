using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.CustomerCustomerDemoCommands;
using WebApiOptimization.Application.Helpers;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.CustomerCustomerDemoHandlers
{
    public class CreateCustomerCustomerDemoHandler : IRequestHandler<CreateCustomerCustomerDemoCommand, ResponseBuilder<CustomerCustomerDemoResponse>>
    {
        private readonly ICustomerCustomerDemoRepository _customerCustomerDemoRepository;

        public CreateCustomerCustomerDemoHandler(ICustomerCustomerDemoRepository customerCustomerDemoRepository)
        {
            _customerCustomerDemoRepository = customerCustomerDemoRepository;
        }

        public async Task<ResponseBuilder<CustomerCustomerDemoResponse>> Handle(CreateCustomerCustomerDemoCommand request, CancellationToken cancellationToken)
        {
            var customerCustomerDemoEntity = CustomerCustomerDemoMapper.Mapper.Map<CustomerCustomerDemo>(request);
            if(customerCustomerDemoEntity == null)
            {
                return new ResponseBuilder<CustomerCustomerDemoResponse> { Message = ResponseBuilderHelper.InvalidData, Data = null };
            }
            try
            {
                var newCustomerCustomerDemo = _customerCustomerDemoRepository.Add(customerCustomerDemoEntity);
                var response = CustomerCustomerDemoMapper.Mapper.Map<CustomerCustomerDemoResponse>(newCustomerCustomerDemo);
                return new ResponseBuilder<CustomerCustomerDemoResponse> { Message ="CustomerCustomerDemo created.", Data = response };

            }
            catch (Exception e)
            {
                return new ResponseBuilder<CustomerCustomerDemoResponse> { Message = $"CustomerCustomerDemo not created! Error: {e.InnerException.Message}", Data = null };
            }
            
        }
    }
}
