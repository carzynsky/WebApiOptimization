using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.CustomerDemographicQueries
{
    public record GetCustomerDemographicByIdQuery(int CustomerTypeId) : IRequest<ResponseBuilder<CustomerDemographicResponse>>;
}
