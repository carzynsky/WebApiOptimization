using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.CustomerCustomerDemoCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.CustomerCustomerDemoHandlers
{
    public class DeleteCustomerCustomerDemoHandler : IRequestHandler<DeleteCustomerCustomerDemoCommand, ResponseBuilder<List<CustomerCustomerDemoResponse>>>
    {
        private readonly ICustomerCustomerDemoRepository _customerCustomerDemoRepository;

        public DeleteCustomerCustomerDemoHandler(ICustomerCustomerDemoRepository customerCustomerDemoRepository)
        {
            _customerCustomerDemoRepository = customerCustomerDemoRepository;
        }

        public async Task<ResponseBuilder<List<CustomerCustomerDemoResponse>>> Handle(DeleteCustomerCustomerDemoCommand request, CancellationToken cancellationToken)
        {
            List<CustomerCustomerDemo> customerCustomerDemosToDelete;
            if(request.CustomerId != null && request.CustomerTypeId == null)
            {
                customerCustomerDemosToDelete = _customerCustomerDemoRepository.GetByCustomerId(request.CustomerId).ToList();
            }
            else if(request.CustomerId == null && request.CustomerTypeId != null)
            {
                customerCustomerDemosToDelete = _customerCustomerDemoRepository.GetByCustomerTypeId(request.CustomerTypeId).ToList();
            }
            else
            {
                customerCustomerDemosToDelete = _customerCustomerDemoRepository.GetByCustomerIdAndCustomerTypeId(request.CustomerId, request.CustomerTypeId).ToList();
            }

            if (!customerCustomerDemosToDelete.Any())
            {
                return new ResponseBuilder<List<CustomerCustomerDemoResponse>> { Message = "No CustomerCustomerDemos found!", Data = null };
            }

            _customerCustomerDemoRepository.DeleteRange(customerCustomerDemosToDelete);
            var response = CustomerCustomerDemoMapper.Mapper.Map<List<CustomerCustomerDemoResponse>>(customerCustomerDemosToDelete);
            return new ResponseBuilder<List<CustomerCustomerDemoResponse>> { Message = "CustomerCustomerDemos deleted.", Data = response };
        }
    }
}
