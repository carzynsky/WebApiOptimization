using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.Customer
{
    public record DeleteCustomerCommand(int Id) : IRequest<CustomerResponse>;
}
