using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.CustomerDemographicQueries
{
    public record GetAllCustomerDemographicsQuery : IRequest<ResponseBuilder<IEnumerable<CustomerDemographicResponse>>>;
}
