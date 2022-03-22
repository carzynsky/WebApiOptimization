using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.Territory
{
    public record GetTerritoryByIdQuery(int Id) : IRequest<TerritoryResponse>;
}
