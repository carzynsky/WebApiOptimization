using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.Territory
{
    public record DeleteTerritoryCommand : IRequest<TerritoryResponse>;
}
