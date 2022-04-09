using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.CustomerQueries
{
    public record GetCustomerByIdQuery(string Id) : IRequest<ResponseBuilder<CustomerResponse>>;
}
