using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.CustomerCustomerDemo
{
    public record GetAllCustomerCustomerDemosQuery : IRequest<IEnumerable<CustomerCustomerDemoResponse>>;
}
