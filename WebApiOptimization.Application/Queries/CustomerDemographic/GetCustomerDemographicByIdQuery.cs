using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.CustomerDemographic
{
    public record GetCustomerDemographicByIdQuery(int CustomerTypeId) : IRequest<CustomerDemographicResponse>;
}
