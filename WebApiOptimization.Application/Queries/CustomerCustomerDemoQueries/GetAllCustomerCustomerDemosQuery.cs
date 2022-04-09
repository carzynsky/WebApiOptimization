using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.CustomerCustomerDemoQueries
{
    public record GetCustomerCustomerDemoQuery(string CustomerId, string CustomerTypeId) : IRequest<ResponseBuilder<IEnumerable<CustomerCustomerDemoResponse>>>;
}
