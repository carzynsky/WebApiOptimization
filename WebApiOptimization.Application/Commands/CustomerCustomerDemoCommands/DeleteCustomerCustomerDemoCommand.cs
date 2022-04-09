using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.CustomerCustomerDemoCommands
{
    public record DeleteCustomerCustomerDemoCommand(string CustomerId, string CustomerTypeId) : IRequest<ResponseBuilder<List<CustomerCustomerDemoResponse>>>;
}
