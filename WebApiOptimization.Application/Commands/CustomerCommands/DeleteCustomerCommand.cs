using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.CustomerCommands
{
    public record DeleteCustomerCommand(string CustomerId) : IRequest<ResponseBuilder<CustomerResponse>>;
}
