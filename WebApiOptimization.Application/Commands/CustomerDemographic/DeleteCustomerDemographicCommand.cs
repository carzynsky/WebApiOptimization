using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.CustomerDemographic
{
    public record DeleteCustomerDemographicCommand(int CustomerTypeId) : IRequest<CustomerDemographicResponse>;
}
