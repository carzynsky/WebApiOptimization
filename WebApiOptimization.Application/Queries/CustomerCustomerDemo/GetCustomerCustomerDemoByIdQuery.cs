using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.CustomerCustomerDemo
{
    public record GetCustomerCustomerDemoByIdQuery(int Id) : IRequest<CustomerCustomerDemoResponse>;
}
