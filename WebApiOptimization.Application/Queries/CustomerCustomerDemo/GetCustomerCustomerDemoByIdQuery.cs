using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.CustomerCustomerDemo
{
    public record GetCustomerCustomerDemoByIdQueryId(int Id) : IRequest<CustomerCustomerDemoResponse>;
}
