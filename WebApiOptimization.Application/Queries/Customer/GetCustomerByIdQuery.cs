using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.Customer
{
    public record GetCustomerByIdQuery(int Id) : IRequest<CustomerResponse>;
}
