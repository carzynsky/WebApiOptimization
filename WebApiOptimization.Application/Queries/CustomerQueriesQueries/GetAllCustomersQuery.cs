using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.CustomerQueries
{
    public record GetAllCustomersQuery : IRequest<ResponseBuilder<IEnumerable<CustomerResponse>>>;
}
