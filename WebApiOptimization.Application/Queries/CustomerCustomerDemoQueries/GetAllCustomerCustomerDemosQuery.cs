using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Filter;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.CustomerCustomerDemoQueries
{
    public class GetCustomerCustomerDemoQuery : PaginationFilter, IRequest<ResponseBuilder<IEnumerable<CustomerCustomerDemoResponse>>>
    {
        public string CustomerId { get; set; }
        public string CustomerTypeId { get; set; }
    }
}
