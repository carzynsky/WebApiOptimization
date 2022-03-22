using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.CustomerCustomerDemo
{
    public class UpdateCustomerCustomerDemoCommand : IRequest<CustomerCustomerDemoResponse>
    {
        public int CustomerId { get; set; }
        public int CustomerTypeId { get; set; }
    }
}
