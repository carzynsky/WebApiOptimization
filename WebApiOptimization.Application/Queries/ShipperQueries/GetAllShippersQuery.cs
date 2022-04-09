using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.ShipperQueries
{
    public record GetAllShippersQuery : IRequest<ResponseBuilder<IEnumerable<ShipperResponse>>>;
}
