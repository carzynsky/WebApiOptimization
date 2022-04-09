using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.TerritoryQueries
{
    public record GetTerritoryByIdQuery(string Id) : IRequest<ResponseBuilder<TerritoryResponse>>;
}
