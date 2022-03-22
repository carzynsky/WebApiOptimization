using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.CustomerCustomerDemo
{
    public record DeleteCustomerCustomerDemoCommand(int Id) : IRequest<CustomerCustomerDemoResponse>;
}
