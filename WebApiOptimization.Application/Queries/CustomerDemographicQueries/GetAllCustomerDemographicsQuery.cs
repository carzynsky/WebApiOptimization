using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Filter;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.CustomerDemographicQueries
{
    public class GetAllCustomerDemographicsQuery : PaginationFilter, IRequest<ResponseBuilder<IEnumerable<CustomerDemographicResponse>>>
    {

    }
}
