using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.ShipperQueries
{
    public record GetShipperByIdQuery(int Id) : IRequest<ResponseBuilder<ShipperResponse>>;
}
