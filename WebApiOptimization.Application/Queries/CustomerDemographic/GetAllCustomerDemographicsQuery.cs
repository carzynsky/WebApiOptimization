using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.CustomerDemographic
{
    public record GetAllCustomerDemographicsQuery : IRequest<IEnumerable<CustomerDemographicResponse>>;
}
