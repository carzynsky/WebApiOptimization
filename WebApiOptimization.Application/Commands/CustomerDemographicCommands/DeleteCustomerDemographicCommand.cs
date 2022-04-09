using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.CustomerDemographicCommands
{
    public record DeleteCustomerDemographicCommand(string CustomerTypeId) : IRequest<ResponseBuilder<CustomerDemographicResponse>>;
}
