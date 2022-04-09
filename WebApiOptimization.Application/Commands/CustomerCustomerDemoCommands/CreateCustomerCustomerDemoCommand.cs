using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.CustomerCustomerDemoCommands
{
    public class CreateCustomerCustomerDemoCommand : IRequest<ResponseBuilder<CustomerCustomerDemoResponse>>
    {
        public int CustomerId { get; set; }
        public int CustomerTypeId { get; set; }
    }
}
