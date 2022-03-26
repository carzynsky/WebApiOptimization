using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.Customer
{
    public record GetAllCustomersQuery : IRequest<IEnumerable<CustomerResponse>>;
}
