using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.CustomerCustomerDemo
{
    public class CreateCustomerCustomerDemoCommand : IRequest<CustomerCustomerDemoResponse>
    {
        public int CustomerId { get; set; }
        public int CustomerTypeId { get; set; }
    }
}
